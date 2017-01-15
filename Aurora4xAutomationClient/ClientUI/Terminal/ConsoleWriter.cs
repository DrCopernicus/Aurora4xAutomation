using System;

namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public class ConsoleWriter : IConsoleWriter
    {
        private readonly object _writeToConsoleLock = new object();

        public int Read()
        {
            return Console.Read();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            lock (_writeToConsoleLock)
            {
                Console.Write(message);
            }
        }

        public void WriteLine(string message)
        {
            lock (_writeToConsoleLock)
            {
                Console.WriteLine(message);
            }
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}