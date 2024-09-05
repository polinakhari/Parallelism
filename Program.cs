using System.Diagnostics;

namespace SpaceCounter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "txt");

            Stopwatch stopwatch = new();
            stopwatch.Start();

            int totalSpaces = await CountSpacesInDirectoryAsync(directoryPath);
            
            stopwatch.Stop();

            Console.WriteLine($"Всего пробелов: {totalSpaces}");
            Console.WriteLine($"Затраченное время: {stopwatch.ElapsedMilliseconds} ms");
        }

        static async Task<int> CountSpacesInFileAsync(string path)
        {
            Console.WriteLine($"Начинаем подсчет в файле {Path.GetFileName(path)}");
            using var reader = new StreamReader(path);
            var content = await reader.ReadToEndAsync();
            var count = content.Count(c => c == ' ');
            Console.WriteLine($"Закончен подсчет в файле {Path.GetFileName(path)}. Пробелов {count}");
            return count;
        }

        static async Task<int> CountSpacesInDirectoryAsync(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            var tasks = files.Select(CountSpacesInFileAsync);
            int[] results = await Task.WhenAll(tasks);
            return results.Sum();
        }
    }
}
