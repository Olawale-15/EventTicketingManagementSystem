namespace EventTicketingApp.Core.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public List<User> Users { get; set; } = new List<User>();
    }
}
