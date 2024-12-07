using EventTicketingApp.Models.AttendeeTicketRecord;
using EventTicketingApp.Models.TicketModel;

namespace EventTicketingApp.Models.AttendeeModel
{
    public class AttendeeResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public List<TicketResponse> Tickets { get; set; }
        public List<AttendeeTicketRecordResponse> TicketRecords { get; set; }
    }
}
