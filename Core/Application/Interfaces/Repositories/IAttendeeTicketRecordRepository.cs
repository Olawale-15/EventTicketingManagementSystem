using EventTicketingApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface IAttendeeTicketRecordRepository
    {
        Task<AttendeeTicketRecord> GetByIdAsync(Guid id);
        Task<AttendeeTicketRecord> GetAsync(Expression<Func<AttendeeTicketRecord, bool>> exp);
        Task<ICollection<AttendeeTicketRecord>> GetAllAsync();
        Task<IEnumerable<AttendeeTicketRecord>> GetByAttendeeIdAsync(Guid attendeeId);
        Task<IEnumerable<AttendeeTicketRecord>> GetByEventIdAsync(Guid eventId);
        Task<AttendeeTicketRecord> AddAsync(AttendeeTicketRecord record);
        Task<AttendeeTicketRecord> UpdateAsync(AttendeeTicketRecord record);
        Task DeleteAsync(AttendeeTicketRecord record);
    }
}
