using EventTicketingApp.Models;
using EventTicketingApp.Models.AttendeeModel;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface IAttendeeService
    {
        Task<BaseResponse> CreateAttendee(CreateAttendeeRequest request);
        Task<BaseResponse<AttendeeResponse>> GetAttendee(Guid id);
        Task<BaseResponse<AttendeeResponse>> GetAttendeeByUserId(Guid id);
        Task<BaseResponse<ICollection<AttendeeResponse>>> GetAllAttendees();
        Task<BaseResponse> UpdateAttendee(Guid id, UpdateAttendeeRequest request);
        Task<BaseResponse> RemoveAttendee(Guid id);
        Task<Guid> GetLoggedInAttendeeId();
    }
}
