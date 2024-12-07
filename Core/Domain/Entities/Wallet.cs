
namespace EventTicketingApp.Core.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Balance { get; set; } = default!;
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
