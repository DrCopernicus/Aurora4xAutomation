using Aurora4xAutomationClient.Common.EArgs;

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

        public void WriteToCurrentLine(object sender, TerminalEventArgs e)
        {
            foreach (var character in e.Message)
            {
                switch (character)
                {
                    case '\b':
                        _terminal.Backspace();
                        break;
                    default:
                        _terminal.AppendToCurrentLine(e.Message);
                        break;
                }
            }
        }

        public void WriteToBuffer(object sender, TerminalEventArgs e)
        {
            _terminal.WriteLine(e.Message, TerminalColor.Default);
        }
    }
}