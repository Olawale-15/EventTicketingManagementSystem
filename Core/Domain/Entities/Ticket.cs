using EventTicketingApp.Core.Domain.Enums;
using System.Net.Sockets;

namespace EventTicketingApp.Core.Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public TicketType Type { get; set; } = default!;
        public Guid EventId { get; set; } = default!;
        public Event Event { get; set; } = default!;
        public Guid? AttendeeId { get; set; } 
        public Attendee? Attendee { get; set; }
        public string? QRCode { get; set; }
        public string? VerificationCode { get; set; }
        public decimal Price { get; set; } 
        public int Count { get; set; }
    }
}
