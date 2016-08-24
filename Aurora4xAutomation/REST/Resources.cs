using System.Net;
using Grapevine;
using Grapevine.Server;

namespace Aurora4xAutomation.REST
{
    public sealed class Resources : RESTResource
    {
        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/$")]
        public void HandleAllGetRequests(HttpListenerContext context)
        {
            SendTextResponse(context, "Server is online!");
        }
    }
}
