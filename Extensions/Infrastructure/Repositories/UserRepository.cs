using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventTicketingAppContext _context;
        public UserRepository(EventTicketingAppContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public async Task<User?> GetAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Wallet)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> exp)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Wallet)
                .FirstOrDefaultAsync(exp);
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Wallet)
                .ToListAsync();
        }

        public async Task RemoveAsync(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return user;
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ExistAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.Email.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
