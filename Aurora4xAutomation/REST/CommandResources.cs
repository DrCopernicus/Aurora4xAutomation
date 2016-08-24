using System;
using System.Linq;
using System.Net;
using Aurora4xAutomation.Automation;
using Aurora4xAutomation.Messages;
using Grapevine;
using Grapevine.Server;

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
            var messages = MessageManagerManager.GetMessagesAfterId(-1, MessageManagerManager.GetLastId());
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
            var messages = MessageManagerManager.GetMessagesAfterId(afterId, uptoId);
            if (messages.Any())
                SendTextResponse(context, string.Join("\n", messages));
            else
                SendTextResponse(context, "");
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/lastmessage")]
        public void HandleLastMessageRequests(HttpListenerContext context)
        {
            SendTextResponse(context, MessageManagerManager.GetLastId().ToString());
        }
    }
}
