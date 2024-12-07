using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventTicketingApp.Infrastructure.Repositories
{
    public class AttendeeTicketRecordRepository : IAttendeeTicketRecordRepository
    {
        private readonly EventTicketingAppContext _context;

        public AttendeeTicketRecordRepository(EventTicketingAppContext context)
        {
            _context = context;
        }

        public async Task<AttendeeTicketRecord?> GetByIdAsync(Guid id)
        {
            return await _context.AttendeeTicketRecords
                .Include(r => r.Attendee)  
                .Include(r => r.Event)      
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<AttendeeTicketRecord?> GetAsync(Expression<Func<AttendeeTicketRecord, bool>> exp)
        {
            return await _context.AttendeeTicketRecords
                .Include(r => r.Attendee)  
                .Include(r => r.Event)     
                .FirstOrDefaultAsync(exp);
        }

        public async Task<ICollection<AttendeeTicketRecord>> GetAllAsync()
        {
            var records =  await _context.AttendeeTicketRecords
                .Include(r => r.Attendee)  
                .Include(r => r.Event)      
                .ToListAsync();
            return records;
        }

        public async Task<IEnumerable<AttendeeTicketRecord>> GetByAttendeeIdAsync(Guid attendeeId)
        {
          var attendeeRecords = await _context.AttendeeTicketRecords
                .Include(r => r.Event)
                .Include(r => r.Ticket)
                .Where(r => r.AttendeeId == attendeeId)
                .ToListAsync();
            return attendeeRecords;
        }

        public async Task<IEnumerable<AttendeeTicketRecord>> GetByEventIdAsync(Guid eventId)
        {
            return await _context.AttendeeTicketRecords
                .Include(r => r.Attendee)
                .Where(r => r.EventId == eventId)
                .ToListAsync();
        }

        public async Task<AttendeeTicketRecord> AddAsync(AttendeeTicketRecord record)
        {
            var result = await _context.AttendeeTicketRecords.AddAsync(record);
            return result.Entity;
        }

        public async Task<AttendeeTicketRecord> UpdateAsync(AttendeeTicketRecord record)
        {
            _context.AttendeeTicketRecords.Update(record);
            return record;
        }

        public async Task DeleteAsync(AttendeeTicketRecord record)
        {
            _context.AttendeeTicketRecords.Remove(record);
        }

    }
}
