using DimonSmart.MazeGenerator;
using MazeRobot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace MazeDemo
{
    class Program
    {
        const int XSize = 9;
        const int YSize = 9;

        static async Task Main(string[] args)
        {
            ConsoleHelper.Initialize(100, 30);





            var kernel = GetKernel();

            var arguments = new KernelArguments { { "label", "label" } };
            var result = await kernel.InvokeAsync("MazeRobot", "LookForward", arguments);
            Console.WriteLine(result);
            Console.ReadLine();

            /*
            var mazePlotter = new MazeConsolePlotter() { WallDrawDelay = TimeSpan.Zero };
            var options = new MazeBuildOptions(100, 100);
            var maze = new Maze<Cell>(XSize, YSize);
            var mazeBuilder = new MazeBuilder(maze, options);
            await mazeBuilder.BuildAsync(mazePlotter);

            var robot = new Robot(maze, 1, 1);

            var mazeEnvironment = new MazeEnvironment(maze, robot);
            var mazeRobotFunctions = new MazeRobotFunctions(mazeEnvironment);

            Console.WriteLine($"Начальное положение робота: ({robot.X}, {robot.Y})");
            var cellAhead = robot.LookForward();
            Console.WriteLine("Содержимое клетки впереди: " + cellAhead.Description);

            robot.MoveRight();
            Console.WriteLine($"После перемещения вправо, положение робота: ({robot.X}, {robot.Y})");

            robot.MarkCell();
            Console.WriteLine("Клетка, в которой находится робот, отмечена.");
            Console.ReadLine();
            */
        }

        private static Kernel GetKernel()
        {
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

            builder.Services.AddLogging(c =>
                c.AddConsole().SetMinimumLevel(LogLevel.Trace));

            var maze = new Maze<Cell>(XSize, YSize);
            var mazePlotter = new MazeConsolePlotter() { WallDrawDelay = TimeSpan.Zero };
            var options = new MazeBuildOptions(0, 0);
            var mazeBuilder = new MazeBuilder(maze, options);
            mazeBuilder.Build(mazePlotter);
            var robot = new Robot(maze, 1, 1);
            var mazeEnvironment = new MazeEnvironment(maze, robot);

            builder.Services.AddSingleton(mazeEnvironment);
            builder.Plugins.AddFromType<MazeRobotFunctions>("MazeRobot");

            var kernel = builder.Build();
            return kernel;
        }
    }
}
