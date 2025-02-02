using DimonSmart.MazeGenerator; // Подключение пакета для генерации лабиринта

namespace MazeDemo
{
    // Класс робота, который перемещается по лабиринту
    public class Robot
    {
        // Текущие координаты робота в лабиринте
        public int X { get; private set; }
        public int Y { get; private set; }

        // Ссылка на лабиринт, в котором перемещается робот
        public Maze<Cell> Maze { get; }

        public Robot(Maze<Cell> maze, int startX, int startY)
        {
            Maze = maze;
            X = startX;
            Y = startY;
        }

        // Методы для наблюдения за клетками в разных направлениях.
        // Для простоты считаем, что робот всегда смотрит "вверх".
        public Cell LookForward() => Maze[X, Y - 1];  // вперед – вверх (y-1)
        public Cell LookLeft() => Maze[X - 1, Y];  // влево – (x-1)
        public Cell LookRight() => Maze[X + 1, Y];  // вправо – (x+1)
        public Cell LookBackward() => Maze[X, Y + 1];  // назад – вниз (y+1)

        // Методы перемещения робота на одну клетку
        public void MoveForward()
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
        public void MoveBackward()
        {
            if (CanMoveTo(X, Y + 1))
            {
                Y = Y + 1;
            }
        }

        // Метод, позволяющий роботу отметить текущую клетку (например, "мелком")
        public void MarkCell()
        {
            var cell = Maze[X, Y];
            cell.Marked = true; // Предполагается, что у Cell есть свойство Marked
        }

        // Приватный метод проверки, можно ли перейти в заданную клетку
        private bool CanMoveTo(int newX, int newY)
        {
            // Проверка границ лабиринта
            if (newX < 0 || newX >= Maze.Width || newY < 0 || newY >= Maze.Height)
            {
                return false;
            }
            // Здесь можно добавить дополнительную проверку (например, наличие стены)
            return !Maze.IsWall(newX, newY);
        }
    }
}
