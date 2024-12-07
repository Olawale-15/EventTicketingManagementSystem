using EventTicketingApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface IEventOrganizerRepository
    {
        Task<EventOrganizer> AddAsync(EventOrganizer eventOrganizer);
        Task<ICollection<EventOrganizer>> GetAllAsync();
        Task<EventOrganizer> GetAsync(Guid id);
        Task<EventOrganizer> GetAsync(Expression<Func<EventOrganizer, bool>> exp);
        Task<bool> ExistsAsync(string email);
        Task RemoveAsync(EventOrganizer eventOrganizer);
        Task<EventOrganizer> UpdateAsync(EventOrganizer eventOrganizer);
    }
}
