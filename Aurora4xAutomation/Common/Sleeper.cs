using System.Threading.Tasks;

namespace Aurora4xAutomation.Common
{
    public class Sleeper : ISleeper
    {
        public void Sleep(int ms)
        {
            Task.Delay(ms).Wait();
        }
    }
}