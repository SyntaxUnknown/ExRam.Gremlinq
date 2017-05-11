﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ExRam.Gremlinq
{
    public struct StepLabel<T> : IGremlinSerializable
    {
        public StepLabel(string label)
        {
            this.Label = label;
        }

        public static StepLabel<T> CreateNew()
        {
            return new StepLabel<T>(Guid.NewGuid().ToString("N"));
        }

        public string Label { get; }

        public (string queryString, IDictionary<string, object> parameters) Serialize(IParameterCache parameterCache, bool inlineParameters)
        {
            if (inlineParameters)
                return ("'" + this.Label + "'", ImmutableDictionary<string, object>.Empty);

            var parameterName = parameterCache.Cache(this.Label);
            return (parameterName, ImmutableDictionary<string, object>.Empty.Add(parameterName, this.Label));
        }
    }
}