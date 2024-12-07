using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Models.TicketModel;

namespace EventTicketingApp.Models.EventModell
{
    public class EventResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = default!;
        public string? Description { get; set; }
        public string? Venue { get; set; } = default!;
        public EventCategory? Category { get; set; } = default!;
        public EventType? Type { get; set; } = default!;
        public EventStatus? Status { get; set; } = default!;
        public DateTime? DateAndTime { get; set; } = default!;
        public TimeSpan? Duration { get; set; } = default!;
        public Guid EventOrganizerId { get; set; } = default!;
        public List<TicketResponse> Tickets { get; set; } 
    }
}
