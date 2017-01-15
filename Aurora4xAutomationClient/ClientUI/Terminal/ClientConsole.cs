
using Aurora4xAutomationClient.ClientUI.Client;

namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public class ClientConsole : IConsole
    {
        private IConsoleWriter _writer;
        private ITerminal _terminal;
        private IClientWrapper _client;

        public ClientConsole(ITerminal terminal, IConsoleWriter writer, IClientWrapper client)
        {
            _terminal = terminal;
            _writer = writer;
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
                        _writer.Write(" \b");
                        break;
                    case '\n':
                        _client.Request(_terminal.GetCurrentLine());
                        _terminal.WriteCurrentLine();
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
            _terminal.WriteLine(message, TerminalColor.Default);
            RewriteConsole();
        }

        public string ReadLine()
        {
            return _writer.ReadLine();
        }

        public int ReadCharacter()
        {
            return _writer.Read();
        }

        private void RewriteConsole()
        {
            _writer.Clear();

            foreach (var message in _terminal.GetBuffer())
                _writer.WriteLine(message.Message);

            _writer.Write("$> ");
            _writer.Write(_terminal.GetCurrentLine());
        }
    }
}