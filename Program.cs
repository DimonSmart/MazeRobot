using DimonSmart.MazeGenerator;

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

            Console.WriteLine($"Начальное положение робота: ({robot.X}, {robot.Y})");
            var cellAhead = robot.LookForward();
            Console.WriteLine("Содержимое клетки впереди: " + cellAhead.Description);

            robot.MoveRight();
            Console.WriteLine($"После перемещения вправо, положение робота: ({robot.X}, {robot.Y})");

            robot.MarkCell();
            Console.WriteLine("Клетка, в которой находится робот, отмечена.");
            Console.ReadLine();
        }
    }
}
