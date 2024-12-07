
using EventTicketingApp.Models;
using EventTicketingApp.Models.WalletModell;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface IWalletService
    {
        Task<BaseResponse> FundWalletAsync(Guid id, decimal amount);
        Task<BaseResponse<WalletResponse>> GetLoggedInUserWalletAsync();
        Task<BaseResponse<WalletResponse>> GetWallet(Guid id);
        Task<BaseResponse<WalletResponse>> GetWalletByUserIdAsync(Guid userId);
        Task<BaseResponse> DeductFromAdminWalletAsync(decimal amount);
        Task<BaseResponse> DeductFromOrganizerWalletAsync(Guid eventOrganizerId, decimal amount);
    }
}
