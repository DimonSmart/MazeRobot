using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MazeRobot.Cache
{
    public class DiskCache
    {
        private readonly string _cacheDir;

        public DiskCache(string cacheDir)
        {
            _cacheDir = cacheDir;
            if (!Directory.Exists(_cacheDir))
            {
                Directory.CreateDirectory(_cacheDir);
            }
        }

        // Вычисление SHA256-хэша строки и преобразование в шестнадцатеричное представление
        private string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Метод получения результата из кэша по промпту
        public async Task<string?> GetAsync(string prompt)
        {
            var hash = ComputeHash(prompt);
            var filePath = Path.Combine(_cacheDir, $"{hash}.json");
            if (File.Exists(filePath))
            {
                using var stream = File.OpenRead(filePath);
                var record = await JsonSerializer.DeserializeAsync<CacheRecord>(stream);
                return record?.Result;
            }
            return null;
        }

        // Метод сохранения результата в кэш
        public async Task SetAsync(string prompt, string result)
        {
            var hash = ComputeHash(prompt);
            var filePath = Path.Combine(_cacheDir, $"{hash}.json");
            var record = new CacheRecord { Prompt = prompt, Result = result };
            using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, record);
        }
    }
}
