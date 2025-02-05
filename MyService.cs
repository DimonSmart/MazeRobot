using Microsoft.Extensions.Logging;

namespace MazeRobot
{
    public class MyService : IMyService
    {
        private readonly ILogger<MyService> _logger;
        public MyService(ILogger<MyService> logger) => _logger = logger;
        public void DoWork() => _logger.LogInformation("IMyService is working.");
    }


}