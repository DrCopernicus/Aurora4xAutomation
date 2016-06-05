using System;
using System.Collections.Concurrent;

namespace Aurora4xAutomation.REST
{
    public static class TicketManager
    {
        private static readonly ConcurrentDictionary<Guid, Ticket> Tickets = new ConcurrentDictionary<Guid, Ticket>();

        public static Ticket CreateTicket()
        {
            var ticket = new Ticket();
            Tickets[ticket.Id] = ticket;
            return ticket;
        }

        public static Ticket GetTicket(Guid guid)
        {
            Ticket ticket;
            if (Tickets.TryGetValue(guid, out ticket))
                return ticket;
            return null;
        }

        public static Ticket GetTicket(string guid)
        {
            Guid parsedGuid;
            if (Guid.TryParse(guid, out parsedGuid))
                return GetTicket(parsedGuid);
            return null;
        }
    }
}
