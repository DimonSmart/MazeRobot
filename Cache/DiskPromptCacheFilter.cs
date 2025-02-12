using Microsoft.SemanticKernel;

namespace MazeRobot.Cache
{
    public sealed class DiskPromptCacheFilter : IPromptRenderFilter
    {
        private readonly DiskCache _diskCache;

        public DiskPromptCacheFilter(DiskCache diskCache)
        {
            _diskCache = diskCache;
        }

        public async Task OnPromptRenderAsync(PromptRenderContext context, Func<PromptRenderContext, Task> next)
        {
            await next(context);
            var promptKey = context.RenderedPrompt ?? "";

            var cachedResult = await _diskCache.GetAsync(promptKey);
            if (cachedResult is not null)
            {
                context.Result = new FunctionResult(context.Function, cachedResult);
            }
        }
    }
}
