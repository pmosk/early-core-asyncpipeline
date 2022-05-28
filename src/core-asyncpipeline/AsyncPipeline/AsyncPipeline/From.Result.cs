using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncResultFlow<TSuccess, TFailure> From<TSuccess, TFailure>(
        ValueTask<Result<TSuccess, TFailure>> valueTask,
        CancellationToken cancellationToken = default)
        where TFailure : struct
        =>
        new(
            asyncPipeline: new(valueTask, cancellationToken));

    public static AsyncResultFlow<TSuccess, TFailure> From<TSuccess, TFailure>(
        Task<Result<TSuccess, TFailure>> task,
        CancellationToken cancellationToken = default)
        where TFailure : struct
    {
        _ = task ?? throw new ArgumentNullException(nameof(task));

        return new(
            asyncPipeline: new(valueTask: new(task), cancellationToken));
    }
}