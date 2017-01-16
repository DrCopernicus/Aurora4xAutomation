using Grapevine;
using Grapevine.Server;
using Server.Events;
using System;
using System.Linq;
using System.Net;

namespace Server.REST
{
    public sealed class CommandResources : RESTResource
    {
        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/command$")]
        public void HandleCommandGetRequests(HttpListenerContext context)
        {
            var command = context.Request.QueryString["q"];

            new Logger().Write(string.Format("[REST] Command: {0}", command));

            RESTManager.CommandFlowManager.QueueCommand(command);
            SendTextResponse(context, "Processed command!");
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/allmessages$")]
        public void HandleAllMessageRequests(HttpListenerContext context)
        {
            var messages = RESTManager.CommandFlowManager.GetMessages(-1);
            if (messages.Any())
                SendTextResponse(context, string.Join("\n", messages));
            else
                SendTextResponse(context, "");
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/messages$")]
        public void HandleMessageRequests(HttpListenerContext context)
        {
            var afterId = Convert.ToInt64(context.Request.QueryString["after"]);
            var uptoId = Convert.ToInt64(context.Request.QueryString["upto"]);
            
            var messages = RESTManager.CommandFlowManager.GetMessages(afterId, uptoId);
            if (messages.Any())
                SendTextResponse(context, string.Join("\n", messages));
            else
                SendTextResponse(context, "");
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/lastmessage")]
        public void HandleLastMessageRequests(HttpListenerContext context)
        {
            SendTextResponse(context, RESTManager.CommandFlowManager.GetLastMessageId().ToString());
        }
    }
}
