using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly EventTicketingAppContext _context;

        public TicketRepository(EventTicketingAppContext context)
        {
            _context = context;
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            return ticket;
        }

        public async Task<ICollection<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.Include(t => t.Event).Include(t => t.Attendee).ToListAsync();
        }

        public async Task<Ticket?> GetAsync(Guid id)
        {
            return await _context.Tickets.Include(t => t.Event).Include(t => t.Attendee).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<ICollection<Ticket>> GetByEventIdAsync(Guid eventId)
        {
            return await _context.Tickets.Where(t => t.EventId == eventId).Include(t => t.Event).Include(t => t.Attendee).ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetByAttendeeIdAsync(Guid attendeeId)
        {
            return await _context.Tickets.Where(t => t.AttendeeId == attendeeId).Include(t => t.Event).Include(t => t.Attendee).ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetByTypeAsync(TicketType type)
        {
            return await _context.Tickets.Where(t => t.Type == type).Include(t => t.Event).Include(t => t.Attendee).ToListAsync();
        }

        public async Task<Ticket?> GetAsync(Expression<Func<Ticket, bool>> exp)
        {
            return await _context.Tickets.Include(t => t.Event).Include(t => t.Attendee).FirstOrDefaultAsync(exp);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Tickets.AnyAsync(t => t.Id == id);
        }

        public async Task RemoveAsync(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }

        public async Task<Ticket> UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            return ticket;
        }
    }
}
