using System;
using System.Diagnostics;
using JetBrains.Annotations;


namespace ErikTheCoder.ServiceContract
{
    // This class writes to the console in a thread-safe manner.
    [UsedImplicitly]
    public static class SafeConsole
    {
        private const string _elapsedSecondsFormat = "00:000";
        private const string _threadIdFormat = "00";
        private static readonly object _lock = new object();


        [UsedImplicitly]
        public static void Write(string Message, ConsoleColor Color = ConsoleColor.White)
        {
            lock (_lock)
            {
                Console.ForegroundColor = Color;
                Console.Write(Message);
                Console.ResetColor();
            }
        }
        

        [UsedImplicitly]
        public static void WriteLine()
        {
            lock (_lock)
            {
                Console.WriteLine();
            }
        }


        [UsedImplicitly]
        public static void WriteLine(string Message, ConsoleColor Color = ConsoleColor.White, Stopwatch Stopwatch = null)
        {
            string elapsed = Stopwatch is null
                ? string.Empty
                : $"{Stopwatch.Elapsed.TotalSeconds.ToString(_elapsedSecondsFormat)}  ";
            lock (_lock)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine($"{elapsed}Thread{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(_threadIdFormat)}  {Message}");
                Console.ResetColor();
            }
        }
    }
}
