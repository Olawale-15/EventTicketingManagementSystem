namespace EventTicketingApp.Models.WalletModell
{
    public class UpdateWalletRequest
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
    }
}
