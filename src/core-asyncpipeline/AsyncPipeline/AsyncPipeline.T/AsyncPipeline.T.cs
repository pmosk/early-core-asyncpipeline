using System.Threading;
using System.Threading.Tasks;

namespace System;

public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
{
    private static CancellationToken CanceledToken() => new(canceled: true);

    private readonly bool isStopped;

    private readonly ValueTask<T> task;

    private readonly CancellationToken cancellationToken;

    // Creates a non-stopped pipeline
    internal AsyncPipeline(ValueTask<T> task, CancellationToken cancellationToken)
    {
        isStopped = false;
        this.task = task;
        this.cancellationToken = cancellationToken;
    }

    // Creates the stopped pipeline
    // The unused arg is intended to separate this constructor from the default constructor
    // which creates the non-stopped pipeline with the task completed state and default result
    private AsyncPipeline(int _)
    {
        isStopped = true;
        task = default;
        cancellationToken = default;
    }
}