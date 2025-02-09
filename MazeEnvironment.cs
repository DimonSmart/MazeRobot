using DimonSmart.MazeGenerator;

namespace MazeRobot
{
    public class MazeEnvironment
    {
        public Maze<Cell> Maze { get; }
        public Robot Robot { get; }

        public MazeEnvironment(Maze<Cell> maze, Robot robot)
        {
            Maze = maze;
            Robot = robot;
        }
    }
}
