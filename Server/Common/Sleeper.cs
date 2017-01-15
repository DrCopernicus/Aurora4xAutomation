using System.Threading.Tasks;

namespace Server.Common
{
    public class Sleeper : ISleeper
    {
        public void Sleep(int ms)
        {
            Task.Delay(ms).Wait();
        }
    }
}