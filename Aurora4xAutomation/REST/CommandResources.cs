using System;
using System.Linq;
using System.Net;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;
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
            CommandParser.Parse(context.Request.QueryString["q"]);
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

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/ticket/[a-zA-Z0-9\-]+$")]
        public void HandleTicketGetRequests(HttpListenerContext context)
        {
            var ticket = TicketManager.GetTicket(context.Request.Url.AbsolutePath.SplitPath()[2]);
            if (ticket == null)
            {
                context.Response.StatusCode = 404;
                SendTextResponse(context, "404 Not Found");
            }
            else if (ticket.Response == Ticket.TicketResponse.Complete)
            {
                context.Response.StatusCode = 404;
                SendTextResponse(context, ticket.Message);
            }
            else if (ticket.Response == Ticket.TicketResponse.Working)
            {
                context.Response.StatusCode = 202;
                SendTextResponse(context, "202 Accepted");
            }
            else
            {
                context.Response.StatusCode = 500;
                SendTextResponse(context, "500 Unknown Ticket State");
            }
        }
    }
}
