using EventTicketingApp.Models.UserModell;
using EventTicketingApp.Models;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponse>> LoginAsync(LoginRequestModel model);
    }
}
