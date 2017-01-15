using System;

namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public class ConsoleFormatter : IConsoleFormatter
    {
        private IConsoleWriter _writer;

        public ConsoleFormatter(IConsoleWriter writer)
        {
            _writer = writer;
        }

        public void WriteLine(TerminalMessage message)
        {
            switch (message.Style)
            {
                case TerminalStyle.Command:
                    _writer.WriteLine(" > " + message.Message);
                    break;
                case TerminalStyle.Error:
                    _writer.ChangeColor(ConsoleColor.Red);
                    _writer.WriteLine(message.Message);
                    _writer.ChangeColor(ConsoleColor.White);
                    break;
                default:
                    _writer.WriteLine(message.Message);
                    break;
            }
        }

        public void Backspace()
        {
            _writer.Write(" \b");
        }

        public string ReadLine()
        {
            return _writer.ReadLine();
        }

        public int Read()
        {
            return _writer.Read();
        }

        public void RewriteConsole(ITerminal terminal)
        {
            _writer.Clear();

            foreach (var message in terminal.GetBuffer())
                WriteLine(message);

            _writer.Write("$> ");
            _writer.Write(terminal.GetCurrentLine());
        }
    }
}