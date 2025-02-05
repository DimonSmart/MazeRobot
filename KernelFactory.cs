using MazeDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace MazeRobot
{
    public static class KernelFactory
    {
        // Example maze-related constants (placeholders)
        public const int XSize = 20;
        public const int YSize = 10;

        public static Kernel BuildKernel(ILoggerFactory loggerFactory)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var builder = Kernel.CreateBuilder();

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:11434"),
                Timeout = TimeSpan.FromMinutes(20)
            };

#pragma warning disable SKEXP0070
            builder.AddOllamaChatCompletion(
                modelId: "llama3.3:70b-instruct-q2_K",
                httpClient: httpClient,
                serviceId: "ollama"
            );
#pragma warning restore SKEXP0070

            // Register the logger factory and logging
            builder.Services.AddSingleton(loggerFactory);
            builder.Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddProvider(new RegionLoggerProvider());
                logging.SetMinimumLevel(LogLevel.Trace);
            });

            // Example: register other services (maze, robot, etc.)
            // var maze = new Maze<Cell>(XSize, YSize);
            // ... additional registrations ...

            // Register a dummy service for demonstration
            builder.Services.AddSingleton<IMyService, MyService>();

            var kernel = builder.Build();
            return kernel;
        }
    }


}