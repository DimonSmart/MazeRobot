using DimonSmart.MazeGenerator;
using System.Text;

namespace MazeRobot
{
    /// <summary>
    /// Extension methods for the IMaze interface.
    /// </summary>
    public static class MazeExtensions
    {
        /// <summary>
        /// Generates a textual representation of the discovered parts of the maze.
        /// </summary>
        /// <param name="maze">The maze to represent.</param>
        /// <returns>A string representing the discovered maze parts.</returns>
        public static string MakeDiscoveredMazePartView(this Maze<Cell> maze)
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    var cell = maze[x, y];

                    if (cell.Discovered)
                    {
                        if (cell.IsWall())
                        {
                            sb.Append("#"); // Wall
                        }
                        else
                        {
                            sb.Append("."); // Path
                        }
                    }
                    else
                    {
                        sb.Append(" "); // Undiscovered
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
