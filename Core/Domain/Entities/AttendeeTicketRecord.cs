using EventTicketingApp.Core.Domain.Enums;

namespace EventTicketingApp.Core.Domain.Entities
{
    public class AttendeeTicketRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AttendeeId { get; set; } = default!;
        public Attendee Attendee { get; set; } = default!;
        public Guid EventId { get; set; } = default!;
        public Event Event { get; set; } = default!;
        public Guid TicketId { get; set; } = default!;
        public Ticket Ticket { get; set; } = default!;
        public int TicketCount { get; set; } 
        public decimal TotalPrice { get; set; }
    }
}
