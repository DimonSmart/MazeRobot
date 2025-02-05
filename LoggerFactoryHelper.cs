using Microsoft.Extensions.Logging;

namespace MazeRobot
{
    public static class LoggerFactoryHelper
    {
        public static ILoggerFactory CreateLoggerFactory()
        {
            return Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder.ClearProviders();
                builder.AddProvider(new RegionLoggerProvider());
                builder.SetMinimumLevel(LogLevel.Trace);
            });
        }
    }

}