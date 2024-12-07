using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly EventTicketingAppContext _context;
        public AttendeeRepository(EventTicketingAppContext context)
        {
            _context = context;
        }
        public async Task<Attendee> AddAsync(Attendee attendee)
        {
            await _context.Attendees.AddAsync(attendee);
            return attendee;
        }

        public async Task<ICollection<Attendee>> GetAllAsync()
        {
            return await _context.Attendees
                .Include(a => a.User)
                .Include(a => a.Tickets)
                .ThenInclude(t => t.Event) 
                .ToListAsync();
        }


        public async Task<Attendee?> GetAsync(Guid id)
        {
            return await _context.Attendees.Include(a => a.User).Include(a => a.Tickets).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Attendee?> GetAsync(Expression<Func<Attendee, bool>> exp)
        {
            return await _context.Attendees.Include(a => a.User).Include(a => a.Tickets).FirstOrDefaultAsync(exp);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Attendees.Include(a => a.User).AnyAsync(a => a.User.Email == email);
        }

        public async Task<bool> ExistsAsync(string email, Guid id)
        {
            var exists = await _context.Attendees.Include(a => a.User).AnyAsync(cus => cus.User.Email == email && cus.Id != id);
            return exists;
        }

        public async Task RemoveAsync(Attendee attendee)
        {
            _context.Attendees.Remove(attendee);
        }

        public async Task<Attendee> UpdateAsync(Attendee attendee)
        {
           _context.Attendees.Update(attendee);
            return attendee;
        }
        public async Task<ICollection<Attendee>> GetByEventIdAsync(Guid eventId)
        {
            return await _context.Attendees
                .Where(a => a.Tickets.Any(t => t.EventId == eventId))
                .Include(a => a.User) 
                .Include(a => a.Tickets)
                    .ThenInclude(t => t.Event)
                .ToListAsync();
        }

        public async Task<List<Attendee>> GetAttendeesByEventIdAsync(Guid eventId)
        {
            return await _context.AttendeeTicketRecords
                .Where(record => record.EventId == eventId)
                .Select(record => record.Attendee)
                .Distinct()
                .Include(attendee => attendee.User) 
                .Include(attendee => attendee.Tickets)  
                .ToListAsync();
        }
    }
}
