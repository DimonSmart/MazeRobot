using DimonSmart.MazeGenerator;

namespace MazeRobot
{
    /// <summary>
    /// Implementation of the ICell interface to represent a maze cell.
    /// </summary>
    public class Cell : ICell
    {
        // Flag indicating whether the cell is a wall.
        private bool _isWall;

        /// <summary>
        /// Allows marking the cell (e.g., by a robot).
        /// </summary>
        public bool Marked { get; set; }

        /// <summary>
        /// Returns a textual description of the cell.
        /// </summary>
        public string Description => _isWall ? "Wall" : "Path";

        /// <summary>
        /// Constructor. By default, the cell is not a wall and not marked.
        /// </summary>
        public Cell()
        {
            _isWall = false;
            Marked = false;
        }

        /// <summary>
        /// Determines whether the cell is a wall.
        /// </summary>
        /// <returns>True if the cell is a wall; otherwise, false.</returns>
        public bool IsWall()
        {
            return _isWall;
        }

        /// <summary>
        /// Makes the cell a wall.
        /// </summary>
        public void MakeWall()
        {
            _isWall = true;
        }
    }
}
