using System;

namespace Aurora4xAutomation.REST
{
    public class Ticket
    {
        public enum TicketResponse
        {
            Started,
            Working,
            Failed,
            Complete
        }

        public Guid Id { get; private set; }
        public string Message { get; set; }
        public TicketResponse Response { get; set; }

        public Ticket()
        {
            Id = Guid.NewGuid();
            Message = "";
            Response = TicketResponse.Started;
        }
    }
}
