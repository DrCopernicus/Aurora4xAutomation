using System;
using Grapevine.Client;

namespace Aurora4xAutomationClient.ClientUI.Client
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