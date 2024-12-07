using EventTicketingApp.Core.Domain.Enums;

namespace EventTicketingApp.Core.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string? Venue { get; set; } 
        public EventCategory Category { get; set; } = default!;
        public EventType Type { get; set; } = default!;
        public EventStatus Status { get; set; } = default!;
        public DateTime DateAndTime { get; set; } = default!;
        public TimeSpan Duration { get; set; } = default!;
        public DateTime EndDate { get; set; } = default!;
        public Guid EventOrganizerId { get; set; } = default!;
        public EventOrganizer Organizer { get; set; } = default!;
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
