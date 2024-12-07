using System.ComponentModel.DataAnnotations;

namespace EventTicketingApp.Models.UserModell
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
