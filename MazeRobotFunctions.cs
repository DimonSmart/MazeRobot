using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MazeDemo
{
    public class MazeRobotFunctions
    {
        private readonly MazeEnvironment _env;

        // Constructor accepts an instance of the environment containing the maze and the robot.
        public MazeRobotFunctions(MazeEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Allows the robot to inspect the cell ahead and returns a description of that cell.
        /// </summary>
        /// <param name="context">Semantic Kernel context containing input data (if necessary).</param>
        /// <returns>Description of the cell ahead.</returns>
        [KernelFunction("LookForward"), Description("Returns the description of the cell directly in front of the robot.")]
        public async Task<string> LookAroundAsync(KernelArguments context)
        {
            _env.Robot.LookAround();
            return _env.Maze.MakeDiscoveredMazePartView();
        }

        /// <summary>
        /// Moves the robot to the right.
        /// </summary>
        [KernelFunction("MoveRight"), Description("Moves the robot one cell to the right if possible.")]
        public async Task<string> MoveRightAsync(KernelArguments context)
        {
            _env.Robot.MoveRight();
            return $"Robot moved to ({_env.Robot.X}, {_env.Robot.Y}).";
        }

     

        // Additional functions can be added: LookLeft, MoveForward, MoveBackward, etc.
    }
}
