
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Models;
using EventTicketingApp.Models.TicketModel;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface ITicketService
    {
        Task<BaseResponse> CreateTicketAsync(CreateTicketRequest request);
        Task<BaseResponse<ICollection<TicketResponse>>> GetTicketsByTypeAsync(Guid EventId, TicketType type);
        Task<BaseResponse<ICollection<TicketResponse>>> GetTicketsByEventIdAsync(Guid eventId);
        Task<BaseResponse<ICollection<TicketResponse>>> GetTicketsByAttendeeIdAsync(Guid attendeeId);
        Task<BaseResponse> UpdateTicketAsync(UpdateTicketRequest request);
        Task<BaseResponse> BuyTicketsAsync(Guid attendeeId, List<BuyTicketRequest> ticketPurchases);
        Task<BaseResponse<ICollection<TicketResponse>>> GetAllAsync();

    }
}
