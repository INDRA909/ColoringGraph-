using System.Diagnostics;

namespace Раскараска_графа
{
    internal class ExtensionsTime
    {
        public static string TimeOut(Stopwatch stopwatch)
        {
            Console.WriteLine();
            string time = $"Время работы в Тиках {stopwatch.ElapsedTicks}\t" +
                              $"Время работы в Миллисекундах {stopwatch.ElapsedTicks / 10000}\t" +
                              $"Время работы в Секундах { stopwatch.ElapsedTicks / 10000000} ";
            return time;
        }
    }
}
