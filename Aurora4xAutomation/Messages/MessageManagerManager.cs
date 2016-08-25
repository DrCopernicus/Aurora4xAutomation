using System;
using System.Collections.Generic;

namespace Aurora4xAutomation.Messages
{
    [Obsolete("very quick patch before i remove this for good!!!")]
    public static class MessageManagerManager
    {
        private static readonly MessageManager Instance = new MessageManager();

        public static List<string> GetMessagesAfterId(long start, long end)
        {
            return Instance.GetMessagesAfterId(start, end);
        }

        public static void AddMessage(string message)
        {
            Instance.AddMessage(message);
        }

        public static long GetLastId()
        {
            return Instance.GetLastId();
        }
    }
}
