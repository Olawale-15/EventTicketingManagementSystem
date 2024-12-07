namespace EventTicketingApp.Core.Domain.Entities
{
    public class EventOrganizer : Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public string? CertificationImage { get; set; } = default!;
        public List<Event> EventsCreated { get; set; } = new List<Event>();
    }
}
