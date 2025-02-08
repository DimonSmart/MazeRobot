using System.Text;

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

        // Helper method to expand tab characters based on the current column position.
        // Each tab advances to the next multiple of tabSize.
        private static string ExpandTabs(string input, int tabSize)
        {
            var result = new StringBuilder();
            int currentColumn = 0;
            foreach (char c in input)
            {
                if (c == '\t')
                {
                    int spacesToAdd = tabSize - (currentColumn % tabSize);
                    result.Append(new string(' ', spacesToAdd));
                    currentColumn += spacesToAdd;
                }
                else
                {
                    result.Append(c);
                    currentColumn++;
                }
            }
            return result.ToString();
        }

        private static void Render()
        {
            int totalWidth = Console.WindowWidth;
            int totalHeight = Console.WindowHeight;
            int regionX = totalWidth / 2;
            int regionWidth = totalWidth - regionX;
            int regionHeight = totalHeight;

            // Draw vertical separator
            for (int y = 0; y < totalHeight; y++)
            {
                Console.SetCursorPosition(totalWidth / 2 - 1, y);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("|");
            }

            // Clear the right region
            for (int y = 0; y < regionHeight; y++)
            {
                Console.SetCursorPosition(regionX, y);
                Console.Write(new string(' ', regionWidth));
            }

            // Format all log entries into individual lines taking into account line breaks and tab expansion
            var formattedLines = new List<(ConsoleColor Color, string Line)>();

            foreach (var entry in _entries)
            {
                // Split the message into lines by newline characters
                var rawLines = entry.Message.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (var rawLine in rawLines)
                {
                    // Expand tab characters properly based on the current column position (tab stops)
                    var line = ExpandTabs(rawLine, 4);

                    // If the line is longer than the region width, perform wrapping
                    if (line.Length > regionWidth)
                    {
                        for (int i = 0; i < line.Length; i += regionWidth)
                        {
                            int segmentLength = Math.Min(regionWidth, line.Length - i);
                            var segment = line.Substring(i, segmentLength);
                            formattedLines.Add((entry.Color, segment.PadRight(regionWidth)));
                        }
                    }
                    else
                    {
                        formattedLines.Add((entry.Color, line.PadRight(regionWidth)));
                    }
                }
            }

            // Display only the last regionHeight lines
            int startLine = Math.Max(0, formattedLines.Count - regionHeight);
            int displayLine = 0;
            for (int i = startLine; i < formattedLines.Count && displayLine < regionHeight; i++)
            {
                var (color, lineText) = formattedLines[i];
                Console.SetCursorPosition(regionX, displayLine);
                Console.ForegroundColor = color;
                Console.Write(lineText);
                displayLine++;
            }
            Console.ResetColor();
        }
    }
}
