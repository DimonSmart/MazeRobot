using Microsoft.SemanticKernel;

namespace MazeRobot.Cache
{
    public sealed class DiskFunctionCacheFilter : IFunctionInvocationFilter
    {
        private readonly DiskCache _diskCache;

        public DiskFunctionCacheFilter(DiskCache diskCache)
        {
            _diskCache = diskCache;
        }

        public async Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
        {
            await next(context);

            if (!string.IsNullOrEmpty(context.Result.RenderedPrompt) && !string.IsNullOrEmpty(context.Result.ToString()))
            {
                await _diskCache.SetAsync(context.Result.RenderedPrompt, context.Result.ToString());
            }
        }
    }
}
