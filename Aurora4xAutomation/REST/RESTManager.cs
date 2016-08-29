using Aurora4xAutomation.Automation;
using Grapevine.Server;

namespace Aurora4xAutomation.REST
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
