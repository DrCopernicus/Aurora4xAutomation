using System.Net;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.Game;
using Grapevine;
using Grapevine.Server;

namespace Aurora4xAutomation.REST.Aurora.Empire
{
    public sealed class EmpireResources : RESTResource
    {
        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/empire/name$")]
        public void HandleAllGetRequests(HttpListenerContext context)
        {
            var ticket = TicketManager.CreateTicket();
            SendTextResponse(context, ticket.Id.ToString());

            Timeline.AddEvent(() =>
            {
                ticket.Response = Ticket.TicketResponse.Working;
                ticket.Message = AuroraGame.Empire.Name.Value;
                ticket.Response = Ticket.TicketResponse.Complete;
            });
        }
    }
}
