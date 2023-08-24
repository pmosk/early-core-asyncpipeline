using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<TResult> PipeValue<TResult>(Func<T, ValueTask<TResult>> pipeAsync)
        =>
        InternalPipeValue(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

    internal AsyncPipeline<TResult> InternalPipeValue<TResult>(Func<T, ValueTask<TResult>> pipeAsync)
        =>
        isStopped is false
            ? new(InnerInvokeValueAsync(pipeAsync), options, cancellationToken)
            : new(default);

    private async ValueTask<TResult> InnerInvokeValueAsync<TResult>(Func<T, ValueTask<TResult>> pipeAsync)
    {
        var result = await valueTask.ConfigureAwait(Options.ContinueOnCapturedContext);
        return await pipeAsync.Invoke(result).ConfigureAwait(Options.ContinueOnCapturedContext);
    }
}