using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Infrastructure.Context;
using EventTicketingApp.Models.TicketModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly EventTicketingAppContext _context;

        public TransactionRepository(EventTicketingAppContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            return transaction;
        }

        public async Task<ICollection<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.Include(t => t.Wallet).ToListAsync();
        }

        public async Task<ICollection<Transaction>> GetAllUserTransactionAsync(Guid userId)
        {
            return await _context.Transactions
                .Include(t => t.Wallet)
                .ThenInclude(w => w.User)
                .Where(t => t.Wallet.UserId == userId)
                .ToListAsync();
        }

        public async Task<Transaction> GetAsync(Guid id)
        {
            return await _context.Transactions.Include(t => t.Wallet).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Transaction> GetAsync(Expression<Func<Transaction, bool>> exp)
        {
            return await _context.Transactions.Include(t => t.Wallet).FirstOrDefaultAsync(exp);
        }

        public async Task<bool> ExistsAsync(DateTime transactionDate)
        {
            return await _context.Transactions.AnyAsync(t => t.TransactionDate.Date == transactionDate.Date);
        }

        public async Task RemoveAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            return transaction;
        }

        public async Task<ICollection<Transaction>> GetAllAsync(Expression<Func<Transaction, bool>> exp)
        {
            var result = await _context.Transactions.Where(exp)
              .Include(t => t.Wallet).ToListAsync();
            return result;
        }

        public async Task<List<Transaction>> GetTicketPurchasesForTransaction(string transactionReference)
        {
            return await _context.Transactions
                .Where(tp => tp.TransactionReference == transactionReference)
                .ToListAsync();
        }
    }
}
