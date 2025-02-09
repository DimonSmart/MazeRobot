using DimonSmart.MazeGenerator;

namespace MazeRobot
{
    // Class representing a robot that moves through the maze
    public class Robot
    {
        // Current coordinates of the robot in the maze
        public int X { get; private set; }
        public int Y { get; private set; }

        public Maze<Cell> Maze { get; }

        public Robot(Maze<Cell> maze, int startX, int startY)
        {
            Maze = maze;
            X = startX;
            Y = startY;
        }

        public void MoveUp()
        {
            if (CanMoveTo(X, Y - 1))
            {
                Y = Y - 1;
            }
        }
        public void MoveLeft()
        {
            if (CanMoveTo(X - 1, Y))
            {
                X = X - 1;
            }
        }
        public void MoveRight()
        {
            if (CanMoveTo(X + 1, Y))
            {
                X = X + 1;
            }
        }
        public void MoveDown()
        {
            if (CanMoveTo(X, Y + 1))
            {
                Y = Y + 1;
            }
        }


        // Method to mark all 9 cells around the robot as discovered
        public void LookAround()
        {
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    var newX = X + dx;
                    var newY = Y + dy;
                    if (newX >= 0 && newX < Maze.Width && newY >= 0 && newY < Maze.Height)
                    {
                        Maze[newX, newY].Discovered = true;
                    }
                }
            }
        }

        private bool CanMoveTo(int newX, int newY)
        {
            if (!Maze.AreCoordinatesValid(newX, newY))
            {
                throw new ArgumentException("Invalid coordinates (mode diag)");
            }

            return !Maze.IsWall(newX, newY);
        }
    }
}
