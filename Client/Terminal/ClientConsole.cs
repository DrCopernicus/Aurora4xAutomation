
using Client.REST;

namespace Client.Terminal
{
    public class ClientConsole : IConsole
    {
        private IConsoleFormatter _formatter;
        private ITerminal _terminal;
        private IClientWrapper _client;

        public ClientConsole(ITerminal terminal, IConsoleFormatter formatter, IClientWrapper client)
        {
            _terminal = terminal;
            _formatter = formatter;
            _client = client;

            RewriteConsole();
        }

        public void WriteToCurrentLine(string message)
        {
            foreach (var character in message)
            {
                switch (character)
                {
                    case '\b':
                        _terminal.Backspace();
                        _formatter.Backspace();
                        break;
                    case '\n':
                        _client.Request(_terminal.GetCurrentLine());
                        _terminal.WriteCurrentLine(TerminalStyle.Command);
                        RewriteConsole();
                        break;
                    default:
                        _terminal.AppendToCurrentLine(character);
                        break;
                }
            }
        }

        public void WriteToBuffer(string message)
        {
            _terminal.WriteLine(message, TerminalStyle.Default);
            RewriteConsole();
        }

        public string ReadLine()
        {
            return _formatter.ReadLine();
        }

        public int ReadCharacter()
        {
            return _formatter.Read();
        }

        private void RewriteConsole()
        {
            _formatter.RewriteConsole(_terminal);
        }
    }
}