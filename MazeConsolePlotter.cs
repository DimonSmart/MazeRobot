using DimonSmart.MazeGenerator;

namespace MazeRobot
{
    public class MazeConsolePlotter : IMazePlotter
    {
        // On some monitors this character printed as not visible
        // so we need a trick with foreground and background colors
        private const string Wall = "▓▓";

        public TimeSpan WallDrawDelay { get; set; } = TimeSpan.FromMilliseconds(25);

        void IMazePlotter.PlotWall(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine(Wall);
            Thread.Sleep(WallDrawDelay);
        }

        void IMazePlotter.PlotPassage(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine(Wall);
        }
    }
}
