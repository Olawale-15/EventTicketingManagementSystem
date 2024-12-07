using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Models;
using EventTicketingApp.Models.AttendeeTicketRecord;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface IAttendeeTicketRecordService
    {
        Task<BaseResponse<AttendeeTicketRecordResponse>> GetByIdAsync(Guid id);
        Task<BaseResponse<IEnumerable<AttendeeTicketRecordResponse>>> GetByAttendeeIdAsync(Guid attendeeId);
        Task<BaseResponse<IEnumerable<AttendeeTicketRecordResponse>>> GetByEventIdAsync(Guid eventId);
        Task<BaseResponse<AttendeeTicketRecordResponse>> AddAsync(AttendeeTicketRecord record);
        Task<BaseResponse<AttendeeTicketRecordResponse>> UpdateAsync(AttendeeTicketRecord record);
        Task<BaseResponse> DeleteAsync(Guid id);
    }

}
