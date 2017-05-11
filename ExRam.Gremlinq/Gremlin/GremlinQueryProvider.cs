using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using LanguageExt;
using Newtonsoft.Json.Linq;

namespace ExRam.Gremlinq
{
    public static class GremlinQueryProvider
    {
        private sealed class JsonSupportGremlinQueryProvider : IGremlinQueryProvider
        {
            private sealed class MemberInfoMappingsContractResolver : DefaultContractResolver
            {
                private readonly IImmutableDictionary<MemberInfo, string> _mappings;

                public MemberInfoMappingsContractResolver(IImmutableDictionary<MemberInfo, string> mappings)
                {
                    this._mappings = mappings;
                }

                protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
                {
                    var property = base.CreateProperty(member, memberSerialization);

                    this._mappings
                        .TryGetValue(member)
                        .IfSome(
                            mapping =>
                            {
                                property.PropertyName = mapping;
                            });

                    return property;
                }
            }

            private readonly IGremlinQueryProvider _baseProvider;

            public JsonSupportGremlinQueryProvider(IGremlinQueryProvider baseProvider)
            {
                this._baseProvider = baseProvider;
            }

            public IGremlinQuery CreateQuery()
            {
                return this._baseProvider
                    .CreateQuery()
                    .ReplaceProvider(this);
            }

            public IAsyncEnumerable<T> Execute<T>(IGremlinQuery<T> query)
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new MemberInfoMappingsContractResolver(query.MemberInfoMappings),
                    TypeNameHandling = TypeNameHandling.Auto,
                };

                return this._baseProvider
                    .Execute(query.Cast<string>())
                    .Select(json => json.StartsWith("{") || json.StartsWith("[")
                        ? JsonConvert.DeserializeObject<T>(this.TransformToken(JToken.Parse(json)).ToString(), settings)
                        : JToken.Parse($"'{json}'").ToObject<T>());
            }

            private JToken TransformToken(JToken token)
            {
                if (token is JObject rootObject)
                    return this.TransformObject(rootObject);

                if (token is JArray array)
                {
                    for (var i = 0; i < array.Count; i++)
                    {
                        array[i] = this.TransformToken(array[i]);
                    }

                    return array;
                }

                return token;
            }

            private JObject TransformObject(JObject obj)
            {
                foreach (var propertyKvp in obj)
                {
                    if (propertyKvp.Value is JObject propertyKvpValue)
                        obj[propertyKvp.Key] = this.TransformObject(propertyKvpValue);
                }

                var type = obj["type"]?.ToString().ToLower();

                if (type == "vertex" || type == "edge")
                {
                    obj.Remove("id");
                    var label = obj["label"]?.ToString();
                    if (label != null)
                    {
                        var maybeSuitableType = type == "vertex"
                            ? this.Model.TryGetVertexTypeOfLabel(label)
                            : this.Model.TryGetEdgeTypeOfLabel(label);

                        maybeSuitableType
                            .IfSome(suitableType =>
                            {
                                obj.AddFirst(new JProperty("$type", suitableType.AssemblyQualifiedName));
                                obj.Remove("type");
                            });
                    }

                    if (obj["properties"] is JObject properties)
                    {
                        foreach (var propertyKvp in properties)
                        {
                            var valueObject = propertyKvp.Value;

                            if (propertyKvp.Value is JArray valueObjectArray)
                                valueObject = valueObjectArray.First;

                            var realValue = (valueObject as JObject)?["value"];
                            if (realValue != null)
                                obj[propertyKvp.Key] = realValue;

                            obj.Remove("properties");
                        }
                    }
                }

                return obj;
            }

            public IGraphModel Model => this._baseProvider.Model;
        }

        private sealed class ModelGremlinQueryProvider : IGremlinQueryProvider
        {
            private readonly IGremlinQueryProvider _baseProvider;

            public ModelGremlinQueryProvider(IGremlinQueryProvider baseProvider, IGraphModel newModel)
            {
                this.Model = newModel;
                this._baseProvider = baseProvider;
            }

            public IGremlinQuery CreateQuery()
            {
                return this._baseProvider
                    .CreateQuery()
                    .ReplaceProvider(this);
            }

            public IAsyncEnumerable<T> Execute<T>(IGremlinQuery<T> query)
            {
                return this._baseProvider.Execute(query);
            }

            public IGraphModel Model { get; }
        }

        public static IAsyncEnumerable<T> Execute<T>(this IGremlinQuery<T> query)
        {
            return query.Provider.Execute(query);
        }

        public static IGremlinQueryProvider WithJsonSupport(this IGremlinQueryProvider provider)
        {
            return new JsonSupportGremlinQueryProvider(provider);
        }

        public static IGremlinQueryProvider WithModel(this IGremlinQueryProvider provider, IGraphModel model)
        {
            return new ModelGremlinQueryProvider(provider, model);
        }
    }
}