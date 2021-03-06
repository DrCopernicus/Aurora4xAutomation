﻿using Client.Terminal;
using Common;
using Grapevine.Client;

namespace Client.REST
{
    public class ClientWrapper : IClientWrapper
    {
        private RESTClient _client;

        public void InitializeConnection(IConsole console)
        {
            var connectionCreator = new ConnectionCreator(console);
            _client = connectionCreator.CreateClient();
        }

        public string GetMessages(string uri, Args args = null)
        {
            var request = new RESTRequest(uri);

            if (args != null)
                foreach (var arg in args)
                    request.AddQuery(arg.Key, arg.Value + "");

            return _client.Execute(request).Content;
        }

        public string Request(string command)
        {
            var res = new RESTRequest("/command");
            res.AddQuery("q", command);
            return _client.Execute(res).Content;
        }
    }
}
