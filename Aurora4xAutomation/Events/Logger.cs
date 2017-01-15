using System;

namespace Aurora4xAutomation.Events
{
    public class Logger : ILogger
    {
        public void Error(string message, string stackTrace)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}\n{1}", message, stackTrace);
            Console.ForegroundColor = previousColor;
        }

        public void Write(string message)
        {
            Console.WriteLine("{0}", message);
        }
    }
}
