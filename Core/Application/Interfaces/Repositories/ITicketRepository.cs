using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> AddAsync(Ticket ticket);
        Task<ICollection<Ticket>> GetAllAsync();
        Task<Ticket> GetAsync(Guid id);
        Task<ICollection<Ticket>> GetByEventIdAsync(Guid eventId);
        Task<ICollection<Ticket>> GetByAttendeeIdAsync(Guid attendeeId);
        Task<ICollection<Ticket>> GetByTypeAsync(TicketType type);
        Task<Ticket> GetAsync(Expression<Func<Ticket, bool>> exp);
        Task<bool> ExistsAsync(Guid id);
        Task RemoveAsync(Ticket ticket);
        Task<Ticket> UpdateAsync(Ticket ticket);
    }
}
