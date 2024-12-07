using EventTicketingApp.Core.Domain.Enums;

namespace EventTicketingApp.Core.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; } = default!;
        public DateTime TransactionDate { get; set; } = default!;
        public TransactionType Type { get; set; } = default!;
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = default!;
       
        // PAYSTACK TRANSACTION
        public string? TransactionReference { get; set; }
        public string? Email  {get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Status { get; set; }
        public Guid? EventId { get; set; }
        public TicketType? Ticket { get; set; }
        public int? Count { get; set; }
    }
}
