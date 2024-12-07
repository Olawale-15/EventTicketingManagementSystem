namespace EventTicketingApp.Core.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Salt { get; set; } = default!;
        public Guid RoleId { get; set; } = default!;
        public Role Role { get; set; } = default!;
        public Wallet Wallet { get; set; } = default!;
    }
}
