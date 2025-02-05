using Microsoft.Extensions.Logging;

namespace MazeRobot
{
    public class RegionLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new RegionLogger(categoryName);
        public void Dispose() { }
    }

}