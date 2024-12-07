using EventTicketingApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(Expression<Func<User, bool>> exp);
        Task<ICollection<User>> GetAllAsync();
        Task RemoveAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> ExistsAsync(string email);
        Task<bool> ExistAsync(string userName);
    }
}
