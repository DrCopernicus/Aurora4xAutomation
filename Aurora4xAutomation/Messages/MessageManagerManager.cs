using System;
using System.Collections.Generic;

namespace Aurora4xAutomation.Messages
{
    [Obsolete("very quick patch before i remove this for good!!!")]
    public static class MessageManagerManager
    {
        private static readonly MessageManager _instance = new MessageManager();

        public static List<string> GetMessagesAfterId(long start, long end)
        {
            return _instance.GetMessagesAfterId(start, end);
        }

        public static void AddMessage(string message)
        {
            _instance.AddMessage(message);
        }

        public static long GetLastId()
        {
            return _instance.GetLastId();
        }
    }
}
