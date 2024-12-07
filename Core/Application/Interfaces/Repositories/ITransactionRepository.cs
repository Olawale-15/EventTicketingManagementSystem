using EventTicketingApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction transaction);
        Task<ICollection<Transaction>> GetAllAsync();
        Task<ICollection<Transaction>> GetAllAsync(Expression<Func<Transaction, bool>> exp);
        Task<Transaction> GetAsync(Guid id);
        Task<Transaction> GetAsync(Expression<Func<Transaction, bool>> exp);
        Task<bool> ExistsAsync(DateTime transactionDate);
        Task RemoveAsync(Transaction transaction);
        Task<Transaction> UpdateAsync(Transaction transaction);
        Task<ICollection<Transaction>> GetAllUserTransactionAsync(Guid userId);
        Task<List<Transaction>> GetTicketPurchasesForTransaction(string transactionReference);
    }
}
