using Aurora4xAutomation.Common;
using Grapevine.Server;

namespace Aurora4xAutomation.REST
{
    public class RESTManager
    {
        public void Begin()
        {
            var server = new RESTServer(host: "*");
            server.Start();

            while (server.IsListening)
            {
                Sleeper.Sleep(1000);
            }

        }
    }
}
