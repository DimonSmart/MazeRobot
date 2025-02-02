using DimonSmart.MazeGenerator; // Подключение пакета для генерации лабиринта

namespace MazeDemo
{
    /// <summary>
    /// Реализация интерфейса ICell для представления клетки лабиринта.
    /// </summary>
    public class Cell : ICell
        {
            // Флаг, указывающий, является ли клетка стеной.
            private bool _isWall;

            /// <summary>
            /// Позволяет пометить клетку (например, роботом).
            /// </summary>
            public bool Marked { get; set; }

            /// <summary>
            /// Возвращает текстовое описание клетки.
            /// </summary>
            public string Description => _isWall ? "Wall" : "Path";

            /// <summary>
            /// Конструктор. По умолчанию клетка не является стеной и не помечена.
            /// </summary>
            public Cell()
            {
                _isWall = false;
                Marked = false;
            }

            /// <summary>
            /// Определяет, является ли клетка стеной.
            /// </summary>
            /// <returns>True, если клетка является стеной; иначе false.</returns>
            public bool IsWall()
            {
                return _isWall;
            }

            /// <summary>
            /// Делает клетку стеной.
            /// </summary>
            public void MakeWall()
            {
                _isWall = true;
            }
        }
}
