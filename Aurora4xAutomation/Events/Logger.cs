using System;

namespace Aurora4xAutomation.Events
{
    public class Logger : ILogger
    {
        public void Handle(Exception e)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}", e.Message);
            Console.ForegroundColor = previousColor;
        }

        public void Write(string message)
        {
            Console.WriteLine("{0}", message);
        }
    }
}
