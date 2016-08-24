using System;
using System.Collections.Generic;
using System.Linq;

namespace Aurora4xAutomation.Messages
{
    public class MessageManager
    {
        private readonly Dictionary<long, string> Messages = new Dictionary<long, string>();
        private long _lastId = -1;

        public List<string> GetMessagesAfterId(long start, long end)
        {
            return Messages.Where(message => message.Key > start && message.Key <= end).Select(message => message.Value).ToList();
        }

        public void AddMessage(string message)
        {
            _lastId++;
            Console.WriteLine("{0}: message", _lastId);
            Messages[_lastId] = message;
        }

        public long GetLastId()
        {
            return _lastId;
        }
    }
}
