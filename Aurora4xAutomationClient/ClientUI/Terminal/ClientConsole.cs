namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public class ClientConsole : IConsole
    {
        private IConsoleWriter _writer;
        private ITerminal _terminal;

        public ClientConsole(ITerminal terminal, IConsoleWriter writer)
        {
            _terminal = terminal;
            _writer = writer;
        }

        public void WriteToCurrentLine(string message)
        {
            foreach (var character in message)
            {
                switch (character)
                {
                    case '\b':
                        _terminal.Backspace();
                        break;
                    default:
                        _terminal.AppendToCurrentLine(character);
                        break;
                }
            }
        }

        public void WriteToBuffer(string message)
        {
            _terminal.WriteLine(message, TerminalColor.Default);
        }

        public string ReadLine()
        {
            return _writer.ReadLine();
        }

        public int ReadCharacter()
        {
            return _writer.Read();
        }
    }
}