using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;

namespace EventTicketingApp.Models.TransactionModel
{
    public class TransactionResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; } = default!;
        public DateTime TransactionDate { get; set; } = default!;
        public TransactionType Type { get; set; } = default!;
        public Guid WalletId { get; set; }
    }
}
