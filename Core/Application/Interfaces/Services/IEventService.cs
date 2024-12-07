using EventTicketingApp.Models.EventModell;
using EventTicketingApp.Models;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Models.AttendeeModel;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface IEventService
    {
        Task<BaseResponse<EventResponse>> CreateEvent(EventRequest request);
        Task<BaseResponse<EventResponse>> GetEventAsync(Guid id);
        Task<BaseResponse<ICollection<EventResponse>>> GetEventsByEventOrganizerId(Guid id);
        Task<BaseResponse<ICollection<EventResponse>>> GetAllEventsAsync();
        Task<BaseResponse<ICollection<AttendeeResponse>>> GetAttendeesByEventIdAsync(Guid eventId);
        Task<BaseResponse> UpdateEventAsync(Guid id, UpdateEventRequest request);
        Task<BaseResponse> RemoveEventAsync(Guid id);
        Task<IEnumerable<EventResponse>> GetAllEventsAsync(string searchQuery);
        Task<BaseResponse<EventResponse>> GetEventByTitleAsync(string title);
        Task<BaseResponse> SendCancellationEmailsAsync(Guid eventId, ICollection<AttendeeResponse> attendees);
        Task<BaseResponse> NotifyAttendeesOnEventUpdateAsync(Guid eventId);
        Task<BaseResponse<ICollection<AttendeeResponse>>> GetAttendeessByEventIdAsync(Guid eventId);

    }
}
