using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;

namespace EventTicketingApp.Models.TicketModel
{
    public class TicketResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public TicketType Type { get; set; } = default!;
        public Guid EventId { get; set; } = default!;
        public Guid AttendeeId { get; set; } = default!;
        public string? QRCode { get; set; }
        public string? VerificationCode { get; set; }
        public string EventName { get; set; }
        public List<TicketTypeCount> TicketTypeCounts { get; set; } = new List<TicketTypeCount>();
    }

    public class TicketTypeCount
    {
        public TicketType Type { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
