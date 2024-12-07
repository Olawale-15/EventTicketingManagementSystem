using EventTicketingApp.Core.Domain.Enums;

namespace EventTicketingApp.Models.AttendeeTicketRecord
{
    public class AttendeeTicketRecordResponse
    {
        public Guid Id { get; set; }
        public Guid AttendeeId { get; set; }
        public string AttendeeName { get; set; }
        public Guid EventId { get; set; }
        public string EventTitle { get; set; } 
        public TicketType Type { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
