using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event _event);
        Task<IQueryable<Event>> GetAllAsync();
        Task<ICollection<Event>> GetAllAsync(Expression<Func<Event, bool>>exp);
        Task<Event> GetAsync(Guid id);
        Task<Event> GetAsync(Expression<Func<Event, bool>> exp);
        Task<bool> ExistsAsync(string title);
        Task RemoveAsync(Event _event);
        Task<Event> UpdateAsync(Event _event);
        Task<List<Event>> SearchEventsAsync(string searchTerm);
        Task<decimal> GetTicketPrice(Guid eventId, TicketType ticketType);

    }
}
