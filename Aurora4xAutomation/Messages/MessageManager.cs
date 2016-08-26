using Aurora4xAutomation.Common.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aurora4xAutomation.Messages
{
    public class MessageManager : IMessageManager
    {
        private readonly Dictionary<long, string> _messages = new Dictionary<long, string>();
        private long _lastId = -1;

        public List<string> GetMessagesAfterId(long start, long end)
        {
            return _messages.Where(message => message.Key > start && message.Key <= end).Select(message => message.Value).ToList();
        }

        public void AddMessage(MessageType type, string message)
        {
            _lastId++;
            Console.WriteLine("{0}: {1}", _lastId, message);
            _messages[_lastId] = string.Format("[{0}] {1}", MessageTypeConverter.ToString(type), message);
        }

        public long GetLastId()
        {
            return _lastId;
        }
    }
}
