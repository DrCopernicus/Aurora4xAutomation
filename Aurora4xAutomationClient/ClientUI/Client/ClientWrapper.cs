using Aurora4xAutomation.Common;
using Grapevine.Client;

namespace Aurora4xAutomationClient.ClientUI.Client
{
    public class ClientWrapper : IClientWrapper
    {
        private RESTClient _client;

        public void InitializeConnection()
        {
            
        }

        public string SendRequest(string uri, Args args = null)
        {
            var request = new RESTRequest("/messages");

            if (args != null)
                foreach (var arg in args)
                    request.AddQuery(arg.Key, (string) arg.Value);

            return _client.Execute(request).Content;
        }
    }
}
