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

            await myService.DoWork();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
