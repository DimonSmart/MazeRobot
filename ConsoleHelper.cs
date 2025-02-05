namespace MazeRobot
{
    public static class ConsoleHelper
    {
        public static void Initialize(int width, int height)
        {
            Console.SetWindowSize(width, height);
            Console.Clear();
        }
    }

}