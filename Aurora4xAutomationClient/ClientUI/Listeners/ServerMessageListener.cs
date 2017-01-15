using Aurora4xAutomationClient.ClientUI.Client;
using Aurora4xAutomationClient.ClientUI.Terminal;
using System.Threading;

namespace Aurora4xAutomationClient.ClientUI.Listeners
{
    public class ServerMessageListener : IInputListener
    {
        private IConsole _console;
        private ServerMessageRetriever _retriever;
        
        public ServerMessageListener(IClientWrapper client, IConsole console)
        {
            _console = console;
            _retriever = new ServerMessageRetriever(client);
        }

        public void BeginListening()
        {
            while (true)
            {
                var messages = _retriever.GetNewMessages();

                foreach (var message in messages)
                    _console.WriteToCurrentLine(message);

                Thread.Sleep(1000);
            }
        }
    }
}