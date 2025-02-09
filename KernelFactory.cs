using DimonSmart.MazeGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Ollama;

namespace MazeRobot
{
    public static class KernelFactory
    {
        // Example maze-related constants (placeholders)
        public const int XSize = 20;
        public const int YSize = 10;

        public static Kernel BuildKernel(ILoggerFactory loggerFactory, MazeConsolePlotter mazeConsolePlotter)
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
            var ollamaSettings = new OllamaPromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            builder.AddOllamaChatCompletion(
                modelId: "llama3.2:3b",
                // modelId: "llama3.3:70b-instruct-q2_K",
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


            builder.Services.AddSingleton<MazeConsolePlotter>(mazeConsolePlotter);
            builder.Services.AddSingleton<IMyService, MyService>();
            var maze = new Maze<Cell>(9, 9);
            new MazeBuilder(maze, new MazeBuildOptions(0, 0)).Build(mazeConsolePlotter);
            var robot = new Robot(maze, 1, 1);
            var mazeEnvironment = new MazeEnvironment(maze, robot);
            builder.Services.AddSingleton(mazeEnvironment);
            builder.Plugins.AddFromType<TimeInformationPlugin>();
            builder.Plugins.AddFromType<MazeWalkerRobotPlugin>("MazeWalkerRobotPlugin");
            var kernel = builder.Build();

            return kernel;
        }
    }
}