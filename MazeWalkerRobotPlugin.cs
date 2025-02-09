using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MazeRobot
{
    public class MazeWalkerRobotPlugin
    {
        private readonly MazeEnvironment _env;

        public MazeWalkerRobotPlugin(MazeEnvironment env)
        {
            _env = env;
        }

        [KernelFunction("LookAround")]
        [Description("Scans the 3x3 grid centered on the robot, marking surrounding cells as discovered, and returns a view of the explored maze area.")]
        public async Task<string> LookAroundAsync(KernelArguments context)
        {
            _env.Robot.LookAround();
            return _env.MakeDiscoveredMazePartView();
        }

        [KernelFunction("MoveRight")]
        [Description("Attempts to move the robot one cell to the right (increases X by 1). Returns the robot's updated coordinates.")]
        public string MoveRight(KernelArguments context)
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

        [KernelFunction("MoveDown")]
        [Description("Attempts to move the robot one cell Down (decreases Y by 1). Returns the robot's updated coordinates.")]
        public async Task<string> MoveDownAsync(KernelArguments context)
        {
            _env.Robot.MoveDown();
            return $"Robot moved to ({_env.Robot.X}, {_env.Robot.Y}).";
        }

        [KernelFunction("MoveUp")]
        [Description("Attempts to move the robot one cell Up (increases Y by 1). Returns the robot's updated coordinates.")]
        public async Task<string> MoveUpAsync(KernelArguments context)
        {
            _env.Robot.MoveUp();
            return $"Robot moved to ({_env.Robot.X}, {_env.Robot.Y}).";
        }
    }
}
