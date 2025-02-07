namespace MazeRobot
{
    public interface IMyService
    {
        Task<string> DoWork(string command);
    }
}