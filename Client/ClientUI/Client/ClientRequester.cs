namespace Client.ClientUI.Client
{
    public class ClientRequester
    {
        private IClientWrapper _client;

        public ClientRequester(IClientWrapper client)
        {
            _client = client;
        }

        private void Request(string command)
        {
            
        }
    }
}