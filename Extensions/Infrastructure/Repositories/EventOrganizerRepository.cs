using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class EventOrganizerRepository : IEventOrganizerRepository
    {
        private readonly EventTicketingAppContext _context;

        public EventOrganizerRepository(EventTicketingAppContext context)
        {
            _context = context;
        }

        public async Task<EventOrganizer> AddAsync(EventOrganizer eventOrganizer)
        {
            await _context.EventOrganizers.AddAsync(eventOrganizer);
            return eventOrganizer;
        }

        public async Task<ICollection<EventOrganizer>> GetAllAsync()
        {
            return await _context.EventOrganizers.Include(eo => eo.User).Include(eo => eo.EventsCreated).ToListAsync();
        }

        public async Task<EventOrganizer> GetAsync(Guid id)
        {
            return await _context.EventOrganizers.Include(eo => eo.User).Include(eo => eo.EventsCreated).FirstOrDefaultAsync(eo => eo.Id == id);
        }

        public async Task<EventOrganizer> GetAsync(Expression<Func<EventOrganizer, bool>> exp)
        {
            return await _context.EventOrganizers.Include(eo => eo.User).Include(eo => eo.EventsCreated).FirstOrDefaultAsync(exp);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.EventOrganizers.Include(eo => eo.User).AnyAsync(eo => eo.User.Email == email);
        }

        public async Task RemoveAsync(EventOrganizer eventOrganizer)
        {
            _context.EventOrganizers.Remove(eventOrganizer);
        }

        public async Task<EventOrganizer> UpdateAsync(EventOrganizer eventOrganizer)
        {
            _context.EventOrganizers.Update(eventOrganizer);
            return eventOrganizer;
        }
    }
}
