using Aurora4xAutomationClient.ClientUI.Terminal;
using Aurora4xAutomationClient.Common.EArgs;
using System;

namespace Aurora4xAutomationClient.ClientUI.Listeners
{
    public class ServerMessageListener : IInputListener
    {
        private IConsole _console;

        public event EventHandler<TerminalEventArgs> ReceivedText;

        public ServerMessageListener(IConsole console)
        {
            _console = console;
        }

        public void BeginListening()
        {
            throw new System.NotImplementedException();
        }
    }
}