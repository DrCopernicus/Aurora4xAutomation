using Aurora4xAutomation.Automation;
using Grapevine;
using Grapevine.Server;
using System;
using System.Linq;
using System.Net;

namespace Aurora4xAutomation.REST
{
    public sealed class CommandResources : RESTResource
    {
        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/command$")]
        public void HandleCommandGetRequests(HttpListenerContext context)
        {
            CommandFlowManager.QueueCommand(context.Request.QueryString["q"]);
            SendTextResponse(context, "Processed command!");
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/allmessages$")]
        public void HandleAllMessageRequests(HttpListenerContext context)
        {
            var messages = CommandFlowManager.GetMessages(-1);
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
            var messages = CommandFlowManager.GetMessages(afterId, uptoId);
            if (messages.Any())
                SendTextResponse(context, string.Join("\n", messages));
            else
                SendTextResponse(context, "");
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/lastmessage")]
        public void HandleLastMessageRequests(HttpListenerContext context)
        {
            SendTextResponse(context, CommandFlowManager.GetLastMessageId().ToString());
        }
    }
}
