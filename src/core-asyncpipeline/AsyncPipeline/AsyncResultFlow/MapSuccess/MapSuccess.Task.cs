#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TFailure> MapSuccess<TResultSuccess>(
            Func<TSuccess, CancellationToken, Task<TResultSuccess>> mapSuccessAsync)
            =>
            InnerMapSuccess(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)));

        private AsyncResultFlow<TResultSuccess, TFailure> InnerMapSuccess<TResultSuccess>(
            Func<TSuccess, CancellationToken, Task<TResultSuccess>> mapSuccessAsync)
            =>
            InnerPipe(
                (r, t) => r.MapSuccessAsync(
                    s => mapSuccessAsync.Invoke(s, t)));
    }
}