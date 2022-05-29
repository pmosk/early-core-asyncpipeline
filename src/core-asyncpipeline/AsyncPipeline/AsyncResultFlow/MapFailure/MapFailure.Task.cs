using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public AsyncResultFlow<TSuccess, TResultFailure> MapFailure<TResultFailure>(
        Func<TFailure, Task<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerMapFailure(
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    private AsyncResultFlow<TSuccess, TResultFailure> InnerMapFailure<TResultFailure>(
        Func<TFailure, Task<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        new(
            asyncPipeline.InternalPipe(
                result => result.MapFailureAsync(mapFailureAsync)));
}