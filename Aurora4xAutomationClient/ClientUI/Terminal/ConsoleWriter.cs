using System;

namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public class ConsoleWriter : IConsoleWriter
    {
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
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}