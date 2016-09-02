using Aurora4xAutomation.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aurora4xAutomationClient.ClientUI
{
    public class ServerMessageRetriever
    {
        private readonly IClientWrapper _clientWrapper;

        public ServerMessageRetriever(IClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }

        public List<string> GetNewMessages()
        {
            var startId = _lastFoundId;
            var endId = GetLastMessageId();
            _lastFoundId = endId;

            var response = _clientWrapper.SendRequest("/messages", new Args {{"after", startId}, {"upto", endId}});

            if (string.IsNullOrEmpty(response))
                return new List<string>();
            return response.Split('\n').ToList();
        }

        private long GetLastMessageId()
        {
            var response = _clientWrapper.SendRequest("/lastmessage");

            return Convert.ToInt64(response);
        }

        private long _lastFoundId = -1;
    }
}
