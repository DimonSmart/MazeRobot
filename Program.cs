using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MazeRobot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            // Configure logging
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddProvider(new RegionLoggerProvider());
                builder.SetMinimumLevel(LogLevel.Trace);
            });

            // Register the Semantic Kernel via a factory that uses DI logger
            services.AddSingleton(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                return KernelFactory.BuildKernel(loggerFactory);
            });

            // Register your application service
            services.AddSingleton<IMyService, MyService>();

            var provider = services.BuildServiceProvider();

            var logger = provider.GetRequiredService<ILogger<Program>>();
            var myService = provider.GetRequiredService<IMyService>();

            logger.LogInformation("Kernel and services are initialized via DI.");

            // Read user command from the console
            Console.WriteLine("Enter command for the robot (or chat):");
            var command = Console.ReadLine() ?? string.Empty;

            var result = await myService.DoWork(command);
            Console.WriteLine("Response:");
            Console.WriteLine(result);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
