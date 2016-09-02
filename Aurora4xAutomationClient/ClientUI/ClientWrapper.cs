using Aurora4xAutomation.Common;
using Grapevine.Client;

namespace Aurora4xAutomationClient.ClientUI
{
    public class ClientWrapper : IClientWrapper
    {
        private readonly RESTClient _client;

        public ClientWrapper(RESTClient client)
        {
            _client = client;
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
