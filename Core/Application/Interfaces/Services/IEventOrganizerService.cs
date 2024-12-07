using EventTicketingApp.Models.EventOrganizerModel;
using EventTicketingApp.Models;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface IEventOrganizerService
    {
        Task<BaseResponse> CreateEventOrganizer(CreateEventOrganizerRequest request);
        Task<BaseResponse<EventOrganizerResponse>> GetEventOrganizer(Guid id);
        Task<BaseResponse<EventOrganizerResponse>> GetEventOrganizerByUserId(Guid id);
        Task<BaseResponse<ICollection<EventOrganizerResponse>>> GetAllEventOrganizers();
        Task<BaseResponse> UpdateEventOrganizer(Guid id, UpdateEventOrganizerRequest request);
        Task<BaseResponse> RemoveEventOrganizer(Guid id);
        Task<Guid> GetLoggedInEventOrganizerId();
    }
}
