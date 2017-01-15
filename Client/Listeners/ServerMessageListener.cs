using System.Threading;
using Client.REST;
using Client.Terminal;

namespace Client.Listeners
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
                    _console.WriteToBuffer(message);

                Thread.Sleep(1000);
            }
        }
    }
}