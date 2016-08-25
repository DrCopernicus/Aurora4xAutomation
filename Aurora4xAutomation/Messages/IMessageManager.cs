using System.Collections.Generic;

namespace Aurora4xAutomation.Messages
{
    public interface IMessageManager
    {
        List<string> GetMessagesAfterId(long start, long end);
        void AddMessage(string message);
        long GetLastId();
    }
}
