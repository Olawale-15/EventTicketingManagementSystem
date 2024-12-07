using EventTicketingApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet> AddAsync(Wallet wallet);
        Task<Wallet> GetAsync(Guid id);
        Task<Wallet> GetAsync(Expression<Func<Wallet, bool>> exp);
        Task RemoveAsync(Wallet wallet);
        Task<Wallet> UpdateAsync(Wallet wallet);
        Task<Wallet> GetAdminWalletAsync();
    }
}
