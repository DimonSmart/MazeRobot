namespace MazeRobot
{
    public class LogEntry
    {
        public string Message { get; }
        public ConsoleColor Color { get; }

        public LogEntry(string message, ConsoleColor color)
        {
            Message = message;
            Color = color;
        }
    }
}