using EventTicketingApp.Models.EventModell;

namespace EventTicketingApp.Models.EventOrganizerModel
{
    public class EventOrganizerResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? CertificationImage { get; set; } = default!;
        public List<EventResponse> Events { get; set; } = new List<EventResponse>();
    }
}
