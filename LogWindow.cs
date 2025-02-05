namespace MazeRobot
{
    public static class LogWindow
    {
        private static readonly List<LogEntry> _entries = new();
        private static readonly object _sync = new();

        public static void AddEntry(LogEntry entry)
        {
            lock (_sync)
            {
                _entries.Add(entry);
                Render();
            }
        }

        private static void Render()
        {
            var totalWidth = Console.WindowWidth;
            var totalHeight = Console.WindowHeight;
            var regionX = totalWidth / 2;
            var regionWidth = totalWidth - regionX;
            var regionHeight = totalHeight;

            // Draw vertical separator
            for (var y = 0; y < totalHeight; y++)
            {
                Console.SetCursorPosition(totalWidth / 2 - 1, y);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("|");
            }

            // Clear the right region
            for (var y = 0; y < regionHeight; y++)
            {
                Console.SetCursorPosition(regionX, y);
                Console.Write(new string(' ', regionWidth));
            }

            var start = Math.Max(0, _entries.Count - regionHeight);
            var line = 0;
            for (var i = start; i < _entries.Count; i++)
            {
                var entry = _entries[i];
                Console.SetCursorPosition(regionX, line);
                Console.ForegroundColor = entry.Color;
                var msg = entry.Message.Length > regionWidth
                    ? entry.Message.Substring(0, regionWidth)
                    : entry.Message.PadRight(regionWidth);
                Console.Write(msg);
                line++;
            }
            Console.ResetColor();
        }
    }
}