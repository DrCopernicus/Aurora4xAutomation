using System;

namespace Aurora4xAutomationClient.Common
{
    public static class EventTools
    {
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
        {
            if (handler != null)
                handler(sender, args);
        }
    }
}