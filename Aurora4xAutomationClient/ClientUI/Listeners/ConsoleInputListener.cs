using Aurora4xAutomationClient.ClientUI.Terminal;

namespace Aurora4xAutomationClient.ClientUI.Listeners
{
    public class ConsoleInputListener : IInputListener
    {
        private IConsole _console;

        public ConsoleInputListener(IConsole console)
        {
            _console = console;
        }

        public void BeginListening()
        {
            while (true)
            {
                var c = _console.ReadCharacter();
                _console.WriteToCurrentLine((char)c + "");
            }
        }
    }
}