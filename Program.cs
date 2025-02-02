using DimonSmart.MazeGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace MazeDemo
{
    class Program
    {
        const int XSize = 10;
        const int YSize = 10;
        static Maze<Cell> _maze = new Maze<Cell>(XSize, YSize);

        static async Task Main(string[] args)
        {
            var mazePlotter = new MazeConsolePlotter();
            var options = new MazeBuildOptions(0, 0);
            var mazeBuilder = new MazeBuilder(_maze, options);
            await mazeBuilder.BuildAsync(mazePlotter);

            var robot = new Robot(_maze, 1, 1);



            var mazeEnvironment = new MazeEnvironment(_maze, robot);
            var mazeRobotFunctions = new MazeRobotFunctions(mazeEnvironment);

            Console.WriteLine($"Начальное положение робота: ({robot.X}, {robot.Y})");
            var cellAhead = robot.LookForward();
            Console.WriteLine("Содержимое клетки впереди: " + cellAhead.Description);

            robot.MoveRight();
            Console.WriteLine($"После перемещения вправо, положение робота: ({robot.X}, {robot.Y})");

            robot.MarkCell();
            Console.WriteLine("Клетка, в которой находится робот, отмечена.");
            Console.ReadLine();
        }

        private static Kernel GetKernel()
        {
            // Загружаем конфигурацию, если требуется (например, для ключей API)
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            // Создаем билдера кернела
            var builder = Kernel.CreateBuilder();

            // Пример: добавляем модель через Ollama (или другой компонент)
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

            // Добавляем логирование
            builder.Services.AddLogging(c =>
                c.AddConsole().SetMinimumLevel(LogLevel.Trace));

            // Собираем кернел
            var kernel = builder.Build();

            // Создаем лабиринт и робота.
            // Используем, например, библиотеку DimonSmart.MazeGenerator для создания лабиринта.
            var mazePlotter = new MazeConsolePlotter();
            var maze = new Maze<Cell>(10, 10);
            var options = new MazeBuildOptions(0, 0);
            var mazeBuilder = new MazeBuilder(maze, options);

            mazeBuilder.Build(mazePlotter);
            var robot = new Robot(maze, 1, 1);

            // Создаем среду, которая инкапсулирует лабиринт и робота.
            var mazeEnvironment = new MazeEnvironment(maze, robot);

            // Создаем плагин с функциями робота.
            var mazeRobotFunctions = new MazeRobotFunctions(mazeEnvironment);

            // Регистрируем плагин в кернеле с именем "MazeRobot"
           // kernel.CreateFunctionFromMethod(mazeRobotFunctions, "MazeRobot");

            return kernel;
        }
    }
}
