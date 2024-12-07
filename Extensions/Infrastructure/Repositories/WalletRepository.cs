using EventTicketingApp.Constant;
using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly EventTicketingAppContext _context;
        public WalletRepository(EventTicketingAppContext context)
        {
            _context = context;
        }

        public async Task<Wallet> AddAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            return wallet;
        }

        public async Task<Wallet?>GetAsync(Guid id)
        {
            return await _context.Wallets
                .Include(w => w.User)
                .Include(w => w.Transactions)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Wallet?> GetAsync(Expression<Func<Wallet, bool>> exp)
        {
            return await _context.Wallets
                .Include(w => w.User)
                .Include(w => w.Transactions)
            .FirstOrDefaultAsync(exp);
        }

        public async Task RemoveAsync(Wallet wallet)
        {
            _context.Wallets.Remove(wallet);
        }

        public async Task<Wallet> UpdateAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            return wallet;
        }

        public async Task<Wallet?> GetAdminWalletAsync()
        {
            return await _context.Wallets
                .Include(w => w.User) 
                .FirstOrDefaultAsync(w => w.User.Role.Name == RoleConstant.Admin);
        }
    }
}
