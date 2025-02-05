using Microsoft.Extensions.Logging;

namespace MazeRobot
{
    public class RegionLogger : ILogger
    {
        private readonly string _categoryName;
        public RegionLogger(string categoryName) => _categoryName = categoryName;
        public IDisposable BeginScope<TState>(TState state) => null!;
        public bool IsEnabled(LogLevel logLevel) => true;
        private static readonly Random _rand = new();

        private ConsoleColor GetRandomColor()
        {
            var colors = new[]
            {
                ConsoleColor.Blue,
                ConsoleColor.Cyan,
                ConsoleColor.DarkCyan,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkYellow,
                ConsoleColor.Gray,
                ConsoleColor.Green,
                ConsoleColor.Magenta,
                ConsoleColor.Red,
                ConsoleColor.White,
                ConsoleColor.Yellow
            };
            return colors[_rand.Next(colors.Length)];
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var message = formatter(state, exception);
            var formatted = $"[{logLevel}] {_categoryName}: {message}";
            var entry = new LogEntry(formatted, GetRandomColor());
            LogWindow.AddEntry(entry);
        }
    }
}