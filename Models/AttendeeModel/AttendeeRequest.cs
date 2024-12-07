using EventTicketingApp.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventTicketingApp.Models.AttendeeModel
{
    public class CreateAttendeeRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = default!;

        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = default!;

        public Gender Gender { get; set; } = default!;
    }

    public class UpdateAttendeeRequest
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}
