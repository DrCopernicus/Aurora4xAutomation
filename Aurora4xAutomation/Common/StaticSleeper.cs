using System;
using System.Threading.Tasks;

namespace Aurora4xAutomation.Common
{
    [Obsolete("Use the standard Sleeper class")]
    public static class StaticSleeper
    {
        public static void Sleep(int ms)
        {
            Task.Delay(ms).Wait();
        }
    }
}
