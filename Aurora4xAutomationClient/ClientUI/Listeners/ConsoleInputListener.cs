using Aurora4xAutomationClient.ClientUI.Terminal;
using Aurora4xAutomationClient.Common;
using Aurora4xAutomationClient.Common.EArgs;
using System;

namespace Aurora4xAutomationClient.ClientUI.Listeners
{
    public class ConsoleInputListener : IInputListener
    {
        private IConsole _console;

        public event EventHandler<TerminalEventArgs> ReceivedText;
        
        public ConsoleInputListener(IConsole console)
        {
            _console = console;
        }

        public void BeginListening()
        {
            while (true)
            {
                var c = Console.Read();
                ReceivedText.Raise(this, new TerminalEventArgs {Message = c + ""});
            }
        }
    }
}