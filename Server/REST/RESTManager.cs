using Grapevine.Server;
using Server.Automation;

namespace Server.REST
{
    public static class RESTManager
    {
        public static void Begin()
        {
            var server = new RESTServer(host: "*");
            server.Start();
        }

        public static CommandFlowManager CommandFlowManager { get; set; }
    }
}
