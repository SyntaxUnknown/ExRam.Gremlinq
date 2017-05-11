﻿using System.Threading;
using System.Threading.Tasks;

namespace ExRam.Gremlinq
{
    public interface IGraphSchemaCreator
    {
        Task CreateSchema(IGraphModel model, CancellationToken ct);
    }
}