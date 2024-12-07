namespace EventTicketingApp.Core.Domain.Entities
{
    public class Attendee : Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public List<AttendeeTicketRecord> TicketRecords { get; set; } = new List<AttendeeTicketRecord>();
    }
}
