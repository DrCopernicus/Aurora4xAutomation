using System.Net;
using Aurora4xAutomation.Command.Parser;
using Grapevine;
using Grapevine.Server;

namespace Aurora4xAutomation.REST
{
    public sealed class CommandResources : RESTResource
    {
        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/command$")]
        public void HandleAllGetRequests(HttpListenerContext context)
        {
            CommandParser.Parse(context.Request.QueryString["q"]);
            SendTextResponse(context, "Processed command!");
        }
    }
}
