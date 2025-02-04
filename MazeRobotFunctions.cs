using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MazeDemo
{
    public class MazeRobotFunctions
    {
        private readonly MazeEnvironment _env;

        // Конструктор принимает экземпляр среды, содержащей лабиринт и робота.
        public MazeRobotFunctions(MazeEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Позволяет роботу осмотреть клетку впереди и возвращает описание этой клетки.
        /// </summary>
        /// <param name="context">Контекст Semantic Kernel, содержащий входные данные (если необходимо).</param>
        /// <returns>Описание клетки, находящейся впереди.</returns>

        [KernelFunction("LookForward"), Description("Returns the description of the cell directly in front of the robot.")]
        public async Task<string> LookForwardAsync(KernelArguments context)
        {
            // Доступ к роботу через _env
            var cell = _env.Robot.LookForward();
            return cell.Description;
        }

        /// <summary>
        /// Перемещает робота вправо.
        /// </summary>
        [KernelFunction("MoveRight"), Description("Moves the robot one cell to the right if possible.")]
        public async Task<string> MoveRightAsync(KernelArguments context)
        {
            _env.Robot.MoveRight();
            return $"Robot moved to ({_env.Robot.X}, {_env.Robot.Y}).";
        }

        /// <summary>
        /// Делает отметку в текущей клетке, где находится робот.
        /// </summary>
        [KernelFunction("MarkCell"), Description("Marks the current cell where the robot is located.")]
        public async Task<string> MarkCellAsync(KernelArguments context)
        {
            _env.Robot.MarkCell();
            return "Current cell marked.";
        }

        // Можно добавить другие функции: LookLeft, MoveForward, MoveBackward и т.д.
    }
}
