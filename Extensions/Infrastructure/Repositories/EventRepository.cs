using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventTicketingAppContext _context;

        public EventRepository(EventTicketingAppContext context)
        {
            _context = context;
        }

        public async Task<Event> AddAsync(Event _event)
        {
            await _context.Events.AddAsync(_event);
            return _event;
        }

        public async Task<IQueryable<Event>> GetAllAsync()
        {
            return _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.Tickets)
                .AsQueryable(); 
        }


        public async Task<Event?> GetAsync(Guid id)
        {
            return await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event?> GetAsync(Expression<Func<Event, bool>> exp)
        {
            return await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(exp);
        }

        public async Task<bool> ExistsAsync(string title)
        {
            return await _context.Events.AnyAsync(e => e.Title == title);
        }

        public async Task RemoveAsync(Event _event)
        {
            _context.Events.Remove(_event);
        }

        public async Task<Event> UpdateAsync(Event _event)
        {
            _context.Events.Update(_event);
            return _event;
        }

        public async Task<ICollection<Event>> GetAllAsync(Expression<Func<Event, bool>> exp)
        {
            return await _context.Events.Where(exp)
                .Include(e => e.Organizer)
                .Include(e => e.Tickets)
                .ToListAsync();
        }

        public async Task<List<Event>> SearchEventsAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await _context.Events.ToListAsync();
            }

            return await _context.Events
                .Where(e => e.Title.Contains(searchTerm) || e.Description.Contains(searchTerm) || e.Venue.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<decimal> GetTicketPrice(Guid eventId, TicketType ticketType)
        {
            var ticket = await _context.Tickets
                .Where(t => t.EventId == eventId && t.Type == ticketType)
                .FirstOrDefaultAsync();

            return ticket?.Price ?? 0;
        }
    }
}
