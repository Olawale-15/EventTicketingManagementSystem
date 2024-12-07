namespace EventTicketingApp.Models.WalletModell
{
    public class WalletResponse
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
