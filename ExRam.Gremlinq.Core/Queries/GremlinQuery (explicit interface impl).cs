﻿// ReSharper disable ArrangeThisQualifier
// ReSharper disable CoVariantArrayConversion

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ExRam.Gremlinq.Core.GraphElements;
using LanguageExt;
using NullGuard;

namespace ExRam.Gremlinq.Core
{
    partial class GremlinQuery<TElement, TOutVertex, TInVertex, TPropertyValue, TMeta, TFoldedQuery> :
        IGremlinQueryAdmin,
        IOrderedGremlinQuery,
        IOrderedElementGremlinQuery,
        IOrderedArrayGremlinQuery<TElement, TFoldedQuery>,
        IOrderedGremlinQuery<TElement>,
        IOrderedElementGremlinQuery<TElement>,
        IOrderedValueGremlinQuery<TElement>,
        IOrderedVertexGremlinQuery,
        IOrderedVertexGremlinQuery<TElement>,
        IOrderedEdgeGremlinQuery,
        IOrderedEdgeGremlinQuery<TElement>,
        IOrderedEdgeGremlinQuery<TElement, TOutVertex>,
        IOrderedInEdgeGremlinQuery<TElement, TInVertex>,
        IOrderedOutEdgeGremlinQuery<TElement, TOutVertex>,
        IOrderedEdgeGremlinQuery<TElement, TOutVertex, TInVertex>,
        IOrderedVertexPropertyGremlinQuery<TElement, TPropertyValue>,
        IOrderedVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>,
        IOrderedPropertyGremlinQuery<TElement>
    {
        IEdgeGremlinQuery<TEdge> IGremlinQuerySource.AddE<TEdge>(TEdge edge) => AddE(edge);

        IEdgeGremlinQuery<TEdge> IGremlinQuerySource.UpdateE<TEdge>(TEdge edge) => UpdateE(edge);

        IEdgeGremlinQuery<TEdge, TElement> IVertexGremlinQuery<TElement>.AddE<TEdge>(TEdge edge) => AddE(edge);

        IEdgeGremlinQuery<TEdge, TElement> IVertexGremlinQuery<TElement>.AddE<TEdge>() => AddE(new TEdge());

        IVertexGremlinQuery<TVertex> IGremlinQuerySource.AddV<TVertex>(TVertex vertex) => AddV(vertex);

        IVertexGremlinQuery<TVertex> IGremlinQuerySource.UpdateV<TVertex>(TVertex vertex) => UpdateV(vertex);

        IGremlinQueryAdmin IGremlinQuery.AsAdmin() => this;

        IVertexGremlinQuery<IVertex> IVertexGremlinQuery.Both() => AddStep<IVertex>(BothStep.NoLabels);

        IVertexGremlinQuery<IVertex> IVertexGremlinQuery.Both<TEdge>() => AddStep<IVertex>(new BothStep(Model.VerticesModel.GetValidFilterLabels(typeof(TEdge))));

        IEdgeGremlinQuery<IEdge> IVertexGremlinQuery.BothE() => AddStep<IEdge>(BothEStep.NoLabels);

        IEdgeGremlinQuery<TEdge> IVertexGremlinQuery.BothE<TEdge>() => AddStep<TEdge>(new BothEStep(Model.EdgesModel.GetValidFilterLabels(typeof(TEdge))));

        IVertexGremlinQuery<IVertex> IEdgeGremlinQuery.BothV() => BothV<IVertex>();

        IVertexGremlinQuery<TVertex> IEdgeGremlinQuery.BothV<TVertex>() => BothV<Unit>().OfType<TVertex>(Model.VerticesModel);

        TTargetQuery IGremlinQueryAdmin.ChangeQueryType<TTargetQuery>() => ChangeQueryType<TTargetQuery>();

        TTargetQuery IValueGremlinQuery<TElement>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IValueGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IValueGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose(predicate, trueChoice, falseChoice);

        TTargetQuery IValueGremlinQuery<TElement>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IValueGremlinQuery<TElement>, TTargetQuery> trueChoice) => Choose(predicate, trueChoice);

        IValueGremlinQuery<TValue> IGremlinQuery.Constant<TValue>(TValue constant) => AddStep<TValue>(new ConstantStep(constant));

        IValueGremlinQuery<long> IGremlinQuery.Count() => AddStep<long, Unit, Unit, Unit, Unit, Unit>(CountStep.Global);

        IValueGremlinQuery<long> IGremlinQuery.CountLocal() => AddStep<long, Unit, Unit, Unit, Unit, Unit>(CountStep.Local);

        IEdgeGremlinQuery<TEdge> IGremlinQuerySource.E<TEdge>(params object[] ids) => AddStep<Unit, Unit, Unit, Unit, Unit, Unit>(new EStep(ids)).OfType<TEdge>(Model.EdgesModel, true);

        IEdgeGremlinQuery<IEdge> IGremlinQuerySource.E(params object[] ids) => AddStep<IEdge, Unit, Unit, Unit, Unit, Unit>(new EStep(ids));

        IGremlinQuery<string> IGremlinQuery.Explain() => AddStep<string, Unit, Unit, Unit, Unit, Unit>(ExplainStep.Instance);

        Task<TElement> IGremlinQuery<TElement>.First() => First(CancellationToken.None);

        Task<TElement> IGremlinQuery<TElement>.First(CancellationToken ct) => First(ct);

        Task<TElement> IGremlinQuery<TElement>.FirstOrDefault() => FirstOrDefault(CancellationToken.None);

        Task<TElement> IGremlinQuery<TElement>.FirstOrDefault(CancellationToken ct) => FirstOrDefault(ct);

        IOutEdgeGremlinQuery<TElement, TNewOutVertex> IEdgeGremlinQuery<TElement>.From<TNewOutVertex>(StepLabel<TNewOutVertex> stepLabel) => AddStep<TElement, TNewOutVertex, Unit, Unit, Unit, Unit>(new FromLabelStep(stepLabel));

        IEdgeGremlinQuery<TElement, TTargetVertex, TOutVertex> IEdgeGremlinQuery<TElement, TOutVertex>.From<TTargetVertex>(StepLabel<TTargetVertex> stepLabel) => AddStep<TElement, TTargetVertex, TOutVertex, Unit, Unit, Unit>(new FromLabelStep(stepLabel));

        IEdgeGremlinQuery<TElement, TTargetVertex, TOutVertex> IEdgeGremlinQuery<TElement, TOutVertex>.From<TTargetVertex>(Func<IVertexGremlinQuery<TOutVertex>, IGremlinQuery<TTargetVertex>> fromVertexTraversal) => AddStep<TElement, TTargetVertex, TOutVertex, Unit, Unit, Unit>(new FromTraversalStep(fromVertexTraversal(Anonymize<TOutVertex, Unit, Unit, Unit, Unit, Unit>())));

        IOutEdgeGremlinQuery<TElement, TNewOutVertex> IEdgeGremlinQuery<TElement>.From<TNewOutVertex>(Func<IGremlinQuery, IGremlinQuery<TNewOutVertex>> fromVertexTraversal) => From<TElement, TNewOutVertex, Unit>(fromVertexTraversal);

        IEdgeGremlinQuery<TElement, TNewOutVertex, TInVertex> IInEdgeGremlinQuery<TElement, TInVertex>.From<TNewOutVertex>(Func<IGremlinQuery, IGremlinQuery<TNewOutVertex>> fromVertexTraversal) => From<TElement, TNewOutVertex, TInVertex>(fromVertexTraversal);

        IAsyncEnumerator<TElement> IAsyncEnumerable<TElement>.GetEnumerator() => GetEnumerator<TElement>();

        IValueGremlinQuery<object> IElementGremlinQuery.Id() => Id();

        IVertexGremlinQuery<IVertex> IVertexGremlinQuery.In() => AddStep<IVertex>(InStep.NoLabels);

        IVertexGremlinQuery<IVertex> IVertexGremlinQuery.In<TEdge>() => AddStep<IVertex>(new InStep(Model.EdgesModel.GetValidFilterLabels(typeof(TEdge))));

        IEdgeGremlinQuery<IEdge> IVertexGremlinQuery.InE() => AddStep<IEdge>(InEStep.NoLabels);

        IEdgeGremlinQuery<TEdge> IVertexGremlinQuery.InE<TEdge>() => AddStep<TEdge>(new InEStep(Model.EdgesModel.GetValidFilterLabels(typeof(TEdge))));

        IInEdgeGremlinQuery<TEdge, TElement> IVertexGremlinQuery<TElement>.InE<TEdge>() => AddStep<TEdge, Unit, TElement, Unit, Unit, Unit>(new InEStep(Model.EdgesModel.GetValidFilterLabels(typeof(TEdge))));

        IGremlinQuery<TNewElement> IGremlinQuerySource.Inject<TNewElement>(params TNewElement[] elements) => Inject(elements);

        IGremlinQuery<TElement> IGremlinQuery<TElement>.Inject(params TElement[] elements) => Inject(elements);

        IValueGremlinQuery<TElement> IValueGremlinQuery<TElement>.Inject(params TElement[] elements) => Inject(elements);

        IGremlinQuery IGremlinQueryAdmin.InsertStep(int index, Step step) => new GremlinQuery<TElement, TOutVertex, TInVertex, TPropertyValue, TMeta, TFoldedQuery>(Model, QueryExecutor, Steps.Insert(index, step), StepLabelMappings, Logger);

        IVertexGremlinQuery<IVertex> IEdgeGremlinQuery.InV() => InV<IVertex>();

        IVertexGremlinQuery<TInVertex> IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>.InV() => InV<TInVertex>();

        IVertexGremlinQuery<TInVertex> IInEdgeGremlinQuery<TElement, TInVertex>.InV() => InV<TInVertex>();

        IVertexGremlinQuery<TVertex> IEdgeGremlinQuery.InV<TVertex>() => InV<Unit>().OfType<TVertex>(Model.VerticesModel);

        IValueGremlinQuery<string> IPropertyGremlinQuery<TElement>.Key() => Key();

        IValueGremlinQuery<string> IElementGremlinQuery.Label() => Label();

        IVertexPropertyGremlinQuery<VertexProperty<TPropertyValue, TNewMeta>, TPropertyValue, TNewMeta> IVertexPropertyGremlinQuery<TElement, TPropertyValue>.Meta<TNewMeta>() => Cast<VertexProperty<TPropertyValue, TNewMeta>, Unit, Unit, TPropertyValue, TNewMeta, Unit>();

        IGraphModel IGremlinQueryAdmin.Model => Model;

        IVertexGremlinQuery<IVertex> IEdgeGremlinQuery.OtherV() => OtherV<IVertex>();

        IVertexGremlinQuery<TVertex> IEdgeGremlinQuery.OtherV<TVertex>() => OtherV<Unit>().OfType<TVertex>(Model.VerticesModel);

        IVertexGremlinQuery<IVertex> IVertexGremlinQuery.Out() => AddStep<IVertex>(OutStep.NoLabels);

        IVertexGremlinQuery<IVertex> IVertexGremlinQuery.Out<TEdge>() => AddStep<IVertex, Unit, Unit, Unit, Unit, Unit>(new OutStep(Model.EdgesModel.GetValidFilterLabels(typeof(TEdge))));

        IEdgeGremlinQuery<TEdge> IVertexGremlinQuery.OutE<TEdge>() => AddStep<TEdge>(new OutEStep(Model.EdgesModel.GetValidFilterLabels(typeof(TEdge))));

        IEdgeGremlinQuery<IEdge> IVertexGremlinQuery.OutE() => AddStep<IEdge>(OutEStep.NoLabels);

        IOutEdgeGremlinQuery<TEdge, TElement> IVertexGremlinQuery<TElement>.OutE<TEdge>() => AddStep<TEdge, TElement, Unit, Unit, Unit, Unit>(new OutEStep(Model.EdgesModel.GetValidFilterLabels(typeof(TEdge))));

        IVertexGremlinQuery<IVertex> IEdgeGremlinQuery.OutV() => OutV<IVertex>();

        IVertexGremlinQuery<TVertex> IEdgeGremlinQuery.OutV<TVertex>() => OutV<Unit>().OfType<TVertex>(Model.VerticesModel);

        IVertexGremlinQuery<TOutVertex> IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>.OutV() => AddStep<TOutVertex, Unit, Unit, Unit, Unit, Unit>(OutVStep.Instance);

        IVertexGremlinQuery<TOutVertex> IOutEdgeGremlinQuery<TElement, TOutVertex>.OutV() => AddStep<TOutVertex, Unit, Unit, Unit, Unit, Unit>(OutVStep.Instance);

        IGremlinQuery<string> IGremlinQuery.Profile() => AddStep<string>(ProfileStep.Instance);

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQuery<TElement>.Properties<TValue>(params string[] keys) => Properties<VertexProperty<TValue>, TValue, Unit>(keys);

        IVertexPropertyGremlinQuery<VertexProperty<object>, object> IVertexGremlinQuery<TElement>.Properties(params string[] keys) => Properties<VertexProperty<object>, object, Unit>(keys);

        IVertexPropertyGremlinQuery<VertexProperty<object>, object> IVertexGremlinQuery<TElement>.Properties() => Properties<VertexProperty<object>, object, Unit>(Array.Empty<string>());

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQuery<TElement>.Properties<TValue>(params Expression<Func<TElement, TValue>>[] projections) => VertexProperties<TValue>(projections);

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQuery<TElement>.Properties<TValue>(params Expression<Func<TElement, TValue[]>>[] projections) => VertexProperties<TValue>(projections);

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQuery<TElement>.Properties<TValue>(params Expression<Func<TElement, VertexProperty<TValue>>>[] projections) => VertexProperties<TValue>(projections);

        IVertexPropertyGremlinQuery<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta> IVertexGremlinQuery<TElement>.Properties<TValue, TNewMeta>(params Expression<Func<TElement, VertexProperty<TValue, TNewMeta>>>[] projections) => Properties<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta>(projections);

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQuery<TElement>.Properties<TValue>(params Expression<Func<TElement, VertexProperty<TValue>[]>>[] projections) => VertexProperties<TValue>(projections);

        IVertexPropertyGremlinQuery<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta> IVertexGremlinQuery<TElement>.Properties<TValue, TNewMeta>(params Expression<Func<TElement, VertexProperty<TValue, TNewMeta>[]>>[] projections) => Properties<VertexProperty<TValue, TNewMeta>, TValue, TNewMeta>(projections);

        IPropertyGremlinQuery<Property<TValue>> IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>.Properties<TValue>(params Expression<Func<TMeta, TValue>>[] projections) => Properties<Property<TValue>, TValue, Unit>(projections);

        IPropertyGremlinQuery<Property<object>> IVertexPropertyGremlinQuery<TElement, TPropertyValue>.Properties(params string[] keys) => Properties<Property<object>, object, Unit>(keys);

        IVertexPropertyGremlinQuery<VertexProperty<TValue>, TValue> IVertexGremlinQuery<TElement>.Properties<TValue>() => Properties<VertexProperty<TValue>, TValue, Unit>(Array.Empty<string>());

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQuery<TElement>.Properties<TValue>(params Expression<Func<TElement, TValue>>[] projections) => Properties<Property<TValue>, TValue, Unit>(projections);

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQuery<TElement>.Properties<TValue>(params Expression<Func<TElement, Property<TValue>>>[] projections) => Properties<Property<TValue>, TValue, Unit>(projections);

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQuery<TElement>.Properties<TValue>(params Expression<Func<TElement, TValue[]>>[] projections) => Properties<Property<TValue>, TValue, Unit>(projections);

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQuery<TElement>.Properties<TValue>(params Expression<Func<TElement, Property<TValue>[]>>[] projections) => Properties<Property<TValue>, TValue, Unit>(projections);

        IPropertyGremlinQuery<Property<TValue>> IVertexPropertyGremlinQuery<TElement, TPropertyValue>.Properties<TValue>(params string[] keys) => Properties<Property<TValue>, Unit, Unit>(keys);

        IVertexPropertyGremlinQuery<VertexProperty<object>, object> IVertexGremlinQuery<TElement>.Properties(params Expression<Func<TElement, VertexProperty<object>>>[] projections) => VertexProperties<object>(projections);

        IPropertyGremlinQuery<Property<object>> IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>.Properties(params string[] keys) => Properties<Property<object>, Unit, Unit>(keys);

        IPropertyGremlinQuery<Property<object>> IEdgeGremlinQuery<TElement>.Properties(params Expression<Func<TElement, Property<object>>>[] projections) => Properties<Property<object>, object, Unit>(projections);

        IPropertyGremlinQuery<Property<object>> IEdgeGremlinQuery<TElement>.Properties() => Properties<Property<object>, Unit, Unit>(Array.Empty<string>());

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQuery<TElement>.Properties<TValue>() => Properties<Property<TValue>, Unit, Unit>(Array.Empty<string>());

        IPropertyGremlinQuery<Property<TValue>> IEdgeGremlinQuery<TElement>.Properties<TValue>(params string[] keys) => Properties<Property<TValue>, Unit, Unit>(keys);

        IPropertyGremlinQuery<Property<object>> IEdgeGremlinQuery<TElement>.Properties(params string[] keys) => Properties<Property<object>, Unit, Unit>(keys);

        IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta> IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>.Property<TValue>(Expression<Func<TMeta, TValue>> projection, TValue value) => Property(projection, value);

        IVertexPropertyGremlinQuery<TElement, TPropertyValue> IVertexPropertyGremlinQuery<TElement, TPropertyValue>.Property(string key, [AllowNull] object value) => Property(key, value);
        
        IVertexGremlinQuery<TElement> IVertexGremlinQuery<TElement>.Property(string key, [AllowNull] object value) => Property(key, value);

        IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta> IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>.Property(string key, [AllowNull] object value) => Property(key, value);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>.Property(string key, [AllowNull] object value) => Property(key, value);

        IEdgeGremlinQuery<TElement, TOutVertex> IEdgeGremlinQuery<TElement, TOutVertex>.Property(string key, [AllowNull] object value) => Property(key, value);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IOutEdgeGremlinQuery<TElement, TOutVertex>.Property(string key, [AllowNull] object value) => Property(key, value);

        IInEdgeGremlinQuery<TElement, TInVertex> IInEdgeGremlinQuery<TElement, TInVertex>.Property(string key, [AllowNull] object value) => Property(key, value);

        IEdgeGremlinQuery<TElement> IEdgeGremlinQuery<TElement>.Property(string key, [AllowNull] object value) => Property(key, value);

        TQuery IGremlinQuery.Select<TQuery, TStepElement>(StepLabel<TQuery, TStepElement> label)
        {
            return this
                .Select<TStepElement>(label)
                .ChangeQueryType<TQuery>();
        }

        IGremlinQuery<TStep> IGremlinQuery.Select<TStep>(StepLabel<TStep> label) => Select<TStep>(label);

        IImmutableDictionary<StepLabel, string> IGremlinQueryAdmin.StepLabelMappings => StepLabelMappings;

        IImmutableList<Step> IGremlinQueryAdmin.Steps => Steps;

        IValueGremlinQuery<TElement> IValueGremlinQuery<TElement>.SumGlobal() => SumGlobal();

        IValueGremlinQuery<TElement> IValueGremlinQuery<TElement>.SumLocal() => SumLocal();

        IInEdgeGremlinQuery<TElement, TNewInVertex> IEdgeGremlinQuery<TElement>.To<TNewInVertex>(StepLabel<TNewInVertex> stepLabel) => To<TElement, Unit, TNewInVertex>(stepLabel);

        IEdgeGremlinQuery<TElement, TOutVertex, TTargetVertex> IEdgeGremlinQuery<TElement, TOutVertex>.To<TTargetVertex>(StepLabel<TTargetVertex> stepLabel) => To<TElement, TOutVertex, TTargetVertex>(stepLabel);

        IEdgeGremlinQuery<TElement, TOutVertex, TTargetVertex> IEdgeGremlinQuery<TElement, TOutVertex>.To<TTargetVertex>(Func<IVertexGremlinQuery<TOutVertex>, IGremlinQuery<TTargetVertex>> toVertexTraversal) => AddStep<TElement, TOutVertex, TTargetVertex, Unit, Unit, Unit>(new ToTraversalStep(toVertexTraversal(Anonymize<TOutVertex, Unit, Unit, Unit, Unit, Unit>())));

        IInEdgeGremlinQuery<TElement, TNewInVertex> IEdgeGremlinQuery<TElement>.To<TNewInVertex>(Func<IGremlinQuery, IGremlinQuery<TNewInVertex>> toVertexTraversal) => To<TElement, Unit, TNewInVertex>(toVertexTraversal);

        IEdgeGremlinQuery<TElement, TOutVertex, TNewInVertex> IOutEdgeGremlinQuery<TElement, TOutVertex>.To<TNewInVertex>(Func<IGremlinQuery, IGremlinQuery<TNewInVertex>> toVertexTraversal) => To<TElement, TOutVertex, TNewInVertex>(toVertexTraversal);

        TFoldedQuery IArrayGremlinQuery<TElement, TFoldedQuery>.Unfold() => Unfold<TFoldedQuery>();

        IVertexGremlinQuery<TVertex> IGremlinQuerySource.V<TVertex>(params object[] ids) => AddStep<Unit, Unit, Unit, Unit, Unit, Unit>(new VStep(ids)).OfType<TVertex>(Model.VerticesModel, true);

        IVertexGremlinQuery<IVertex> IGremlinQuerySource.V(params object[] ids) => AddStep<IVertex, Unit, Unit, Unit, Unit, Unit>(new VStep(ids));

        IValueGremlinQuery<TPropertyValue> IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>.Value() => Value<TPropertyValue>();

        IValueGremlinQuery<TValue> IPropertyGremlinQuery<TElement>.Value<TValue>() => Value<TValue>();

        IValueGremlinQuery<TPropertyValue> IVertexPropertyGremlinQuery<TElement, TPropertyValue>.Value() => Value<TPropertyValue>();

        IValueGremlinQuery<object> IPropertyGremlinQuery<TElement>.Value() => Value<object>();

        IGremlinQuery<TMeta> IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>.ValueMap() => ValueMap<TMeta>(Array.Empty<string>());

        IValueGremlinQuery<IDictionary<string, TTarget>> IElementGremlinQuery.ValueMap<TTarget>(params string[] keys) => ValueMap<IDictionary<string, TTarget>>(keys);

        IValueGremlinQuery<IDictionary<string, object>> IElementGremlinQuery.ValueMap(params string[] keys) => ValueMap<IDictionary<string, object>>(keys);

        IValueGremlinQuery<TValue> IVertexGremlinQuery<TElement>.Values<TValue, TNewMeta>(params Expression<Func<TElement, VertexProperty<TValue, TNewMeta>>>[] projections) => ValuesForProjections<TValue>(projections);

        IValueGremlinQuery<TValue> IVertexGremlinQuery<TElement>.Values<TValue>(params Expression<Func<TElement, VertexProperty<TValue>>>[] projections) => ValuesForProjections<TValue>(projections);

        IValueGremlinQuery<TValue> IEdgeGremlinQuery<TElement>.Values<TValue>(params Expression<Func<TElement, Property<TValue>>>[] projections) => ValuesForProjections<TValue>(projections);

        IValueGremlinQuery<TTarget> IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>.Values<TTarget>(params Expression<Func<TMeta, TTarget>>[] projections) => ValuesForProjections<TTarget>(projections);

        IValueGremlinQuery<TTarget> IVertexGremlinQuery<TElement>.Values<TTarget>(params Expression<Func<TElement, VertexProperty<TTarget>[]>>[] projections) => ValuesForProjections<TTarget>(projections);

        IValueGremlinQuery<TTarget> IVertexGremlinQuery<TElement>.Values<TTarget, TTargetMeta>(params Expression<Func<TElement, VertexProperty<TTarget, TTargetMeta>[]>>[] projections) => ValuesForProjections<TTarget>(projections);

        IValueGremlinQuery<TTarget> IEdgeGremlinQuery<TElement>.Values<TTarget>(params Expression<Func<TElement, Property<TTarget>[]>>[] projections) => ValuesForProjections<TTarget>(projections);

        IValueGremlinQuery<TValue> IElementGremlinQuery.Values<TValue>(params string[] keys) => ValuesForKeys<TValue>(keys);

        IValueGremlinQuery<object> IElementGremlinQuery.Values(params string[] keys) => ValuesForKeys<object>(keys);

        IValueGremlinQuery<object> IVertexGremlinQuery<TElement>.Values(params Expression<Func<TElement, VertexProperty<object>>>[] projections) => ValuesForProjections<object>(projections);

        IValueGremlinQuery<object> IEdgeGremlinQuery<TElement>.Values(params Expression<Func<TElement, Property<object>>>[] projections) => ValuesForProjections<object>(projections);

        IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta> IVertexPropertyGremlinQuery<TElement, TPropertyValue, TMeta>.Where(Expression<Func<VertexProperty<TPropertyValue, TMeta>, bool>> predicate) => Where(predicate);

        IValueGremlinQuery<TElement> IValueGremlinQuery<TElement>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);

        IPropertyGremlinQuery<TElement> IPropertyGremlinQuery<TElement>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
    }
}
