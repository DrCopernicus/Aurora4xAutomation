using Grapevine.Server;

namespace Aurora4xAutomation.REST
{
    public class RESTManager
    {
        public void Begin()
        {
            var server = new RESTServer(host: "*");
            server.Start();
        }
    }
}
