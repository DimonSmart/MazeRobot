using DimonSmart.MazeGenerator;
using System;
using System.Text;

namespace MazeRobot
{
    /// <summary>
    /// Extension methods for MazeEnvironment.
    /// </summary>
    public static class MazeExtensions
    {
        /// <summary>
        /// Generates a textual representation of the discovered parts of the maze.
        /// </summary>
        /// <param name="mazeEnv">The maze environment containing the maze and the robot.</param>
        /// <param name="encloseSymbolsInQuotes">
        /// If true, each cell symbol is enclosed in quotes (e.g., 'R', '#' etc.).
        /// </param>
        /// <returns>A string representing the discovered maze parts.</returns>
        public static string MakeDiscoveredMazePartView(this MazeEnvironment mazeEnv, bool encloseSymbolsInQuotes = false)
        {
            if (mazeEnv == null)
                throw new ArgumentNullException(nameof(mazeEnv));
            if (mazeEnv.Maze == null)
                throw new ArgumentNullException(nameof(mazeEnv.Maze));

            var maze = mazeEnv.Maze;
            StringBuilder sb = new StringBuilder();

            int width = maze.Width;
            int height = maze.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var cell = maze[x, y];
                    char symbol = GetCellSymbol(cell, x, y, mazeEnv.Robot);

                    if (encloseSymbolsInQuotes)
                    {
                        sb.Append($"'{symbol}'");
                    }
                    else
                    {
                        sb.Append(symbol);
                    }
                }
                sb.AppendLine();
            }
            var result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Determines the symbol to use for a maze cell.
        /// </summary>
        /// <param name="cell">The maze cell.</param>
        /// <param name="x">The x coordinate of the cell.</param>
        /// <param name="y">The y coordinate of the cell.</param>
        /// <param name="robot">The robot in the maze environment.</param>
        /// <returns>A character representing the cell.</returns>
        private static char GetCellSymbol(Cell cell, int x, int y, Robot robot)
        {
            if (robot != null && x == robot.X && y == robot.Y)
                return 'R';

            // Если ячейка ещё не открыта, возвращаем пробел (можно заменить на другой символ)
            if (!cell.Discovered)
                return '?';

            // Если ячейка обнаружена, возвращаем '#' для стены и '_' для прохода
            return cell.IsWall() ? '#' : '_';
        }
    }
}
