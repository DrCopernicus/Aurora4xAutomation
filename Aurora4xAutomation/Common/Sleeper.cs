using System.Threading.Tasks;

namespace Aurora4xAutomation.Common
{
    public static class Sleeper
    {
        public static void Sleep(int ms)
        {
            Task.Delay(ms).Wait();
        }
    }
}
