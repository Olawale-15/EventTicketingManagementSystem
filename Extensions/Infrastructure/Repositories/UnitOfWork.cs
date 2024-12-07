using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Infrastructure.Context;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventTicketingAppContext _context;
        public UnitOfWork(EventTicketingAppContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
