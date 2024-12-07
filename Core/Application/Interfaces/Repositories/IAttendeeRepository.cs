using EventTicketingApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface IAttendeeRepository
    {
        Task<Attendee> AddAsync(Attendee attendee);
        Task<ICollection<Attendee>> GetAllAsync();
        Task<Attendee> GetAsync(Guid id);
        Task<Attendee> GetAsync(Expression<Func<Attendee, bool>> exp);
        Task<bool> ExistsAsync(string email);
        Task<bool> ExistsAsync(string email, Guid id);
        Task RemoveAsync(Attendee attendee);
        Task<Attendee> UpdateAsync(Attendee attendee);
        Task<ICollection<Attendee>> GetByEventIdAsync(Guid eventId);
        Task<List<Attendee>> GetAttendeesByEventIdAsync(Guid eventId);
    }
}
