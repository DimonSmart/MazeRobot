using Microsoft.Extensions.AI;
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

            // Example: register other services (maze, robot, etc.)
            // var maze = new Maze<Cell>(XSize, YSize);
            // ... additional registrations ...

            // Register a dummy service for demonstration
            builder.Services.AddSingleton<IMyService, MyService>();
            builder.Plugins.AddFromType<TimeInformationPlugin>();

            var kernel = builder.Build();

            // Retrieve the MazeRobotFunctions service and import it as a skill
            var robotFunctions = kernel.Services.GetService<MazeRobotFunctions>();
            // If you see 'ImportSkillFromInstance' is missing, use 'ImportSkill' or 'ImportFunctions'
            // kernel.ImportPluginFromFunctions(robotFunctions, skillName: "MazeRobot");

            // Create a plugin from a class that contains kernel functions
            // var timeInformationPlugin = KernelPluginFactory.CreateFromType<TimeInformationPlugin>("TimeInformation");
            // kernel.Plugins.AddFromObject(timeInformationPlugin);

            return kernel;
        }
    }


}