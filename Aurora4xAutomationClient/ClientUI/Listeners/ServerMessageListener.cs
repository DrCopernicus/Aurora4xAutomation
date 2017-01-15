using Aurora4xAutomationClient.ClientUI.Client;

namespace Aurora4xAutomationClient.ClientUI.Listeners
{
    public class ServerMessageListener : IInputListener
    {
        private ServerMessageRetriever _retriever;
        
        public ServerMessageListener(IClientWrapper client)
        {
            _retriever = new ServerMessageRetriever(client);
        }

        public void BeginListening()
        {
            throw new System.NotImplementedException();
        }
    }
}