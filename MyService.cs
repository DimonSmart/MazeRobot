using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;


namespace MazeRobot
{
    public class MyService : IMyService
    {
        private readonly ILogger<MyService> _logger;
        private readonly MazeConsolePlotter _plotter;
        private readonly Kernel _kernel;

        public MyService(ILogger<MyService> logger, MazeConsolePlotter plotter, Kernel kernel)
        {
            _logger = logger;
            _plotter = plotter;
            _kernel = kernel;
        }

        public async Task DoWork()
        {

            string systemPrompt = @"
You are a chatbot capable of controlling a robot in a maze.
The robot understands and can execute the following commands: move up, down, left, right.
The robot cannot pass through walls.
When issued the 'look around' command, the robot look around for 1 cell.

In the grid:
  '?' indicates an unexplored cell.
  '#' indicates a wall.
  '_' indicates an open passage.
  'R' indicates the robot's current position.
  Any discovered object (e.g., 'apple') is labeled with its letter 'A' for apple.
Once a cell is discovered, it remains visible permanently.
";

            // Call the semantic kernel to process the prompt.
            // (Assuming _kernel.RunAsync(string) returns the generated response.)
            // Assuming _kernel is an instance of Kernel

            var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

            ChatHistory chatHistory = [];
            chatHistory.AddSystemMessage(systemPrompt);
            string? input = null;
            while (true)
            {
                Console.Write("\nUser > ");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    // Leaves if the user hit enter without typing any word
                    break;
                }
                chatHistory.AddUserMessage(input);
                var chatResult = await chatCompletionService
                 .GetChatMessageContentAsync(chatHistory, new PromptExecutionSettings
                 {
                     FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                 }, _kernel);
                Console.Write($"\nAssistant > {chatResult}\n");
            }
        }
    }
}