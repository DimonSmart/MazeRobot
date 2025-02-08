using MazeDemo;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MazeRobot
{
    public class MazeRobotFunctions
    {
        private readonly MazeEnvironment _env;

        public MazeRobotFunctions(MazeEnvironment env)
        {
            _env = env;
        }

        [KernelFunction("LookAround")]
        [Description("Scans the 3x3 grid centered on the robot, marking surrounding cells as discovered, and returns a view of the explored maze area.")]
        public async Task<string> LookAroundAsync(KernelArguments context)
        {
            _env.Robot.LookAround();
            return _env.Maze.MakeDiscoveredMazePartView();
        }

        [KernelFunction("MoveRight")]
        [Description("Attempts to move the robot one cell to the right (increases X by 1). Returns the robot's updated coordinates.")]
        public async Task<string> MoveRightAsync(KernelArguments context)
        {
            _env.Robot.MoveRight();
            return $"Robot moved to ({_env.Robot.X}, {_env.Robot.Y}).";
        }

        [KernelFunction("MoveLeft")]
        [Description("Attempts to move the robot one cell to the left (decreases X by 1). Returns the robot's updated coordinates.")]
        public async Task<string> MoveLeftAsync(KernelArguments context)
        {
            _env.Robot.MoveLeft();
            return $"Robot moved to ({_env.Robot.X}, {_env.Robot.Y}).";
        }

        [KernelFunction("MoveForward")]
        [Description("Attempts to move the robot one cell forward (decreases Y by 1). Returns the robot's updated coordinates.")]
        public async Task<string> MoveForwardAsync(KernelArguments context)
        {
            _env.Robot.MoveForward();
            return $"Robot moved to ({_env.Robot.X}, {_env.Robot.Y}).";
        }

        [KernelFunction("MoveBackward")]
        [Description("Attempts to move the robot one cell backward (increases Y by 1). Returns the robot's updated coordinates.")]
        public async Task<string> MoveBackwardAsync(KernelArguments context)
        {
            _env.Robot.MoveBackward();
            return $"Robot moved to ({_env.Robot.X}, {_env.Robot.Y}).";
        }
    }
}
