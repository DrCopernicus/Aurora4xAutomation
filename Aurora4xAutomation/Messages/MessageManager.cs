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
            _messages[_lastId] = string.Format("[{0}] {1}", MessageTypeToString(type), message);
        }

        public long GetLastId()
        {
            return _lastId;
        }

        private string MessageTypeToString(MessageType type)
        {
            switch (type)
            {
                case MessageType.Error:
                    return "CRIT";
                case MessageType.Information:
                    return "INFO";
                case MessageType.Warning:
                    return "WARN";
            }
            throw new Exception(string.Format("Tried to print message with unhandled type: {0}", type));
        }
    }
}
