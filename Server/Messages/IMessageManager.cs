using System.Collections.Generic;

namespace Server.Messages
{
    public interface IMessageManager
    {
        List<string> GetMessagesAfterId(long start, long end);
        void AddMessage(MessageType type, string message);
        long GetLastId();
    }
}
