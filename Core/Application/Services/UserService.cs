using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Infrastructure.Repositories;
using EventTicketingApp.Models;
using EventTicketingApp.Models.UserModell;

namespace EventTicketingApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<UserResponse>> LoginAsync(LoginRequestModel model)
        {
            var user = await _userRepository.GetAsync(u => u.Email == model.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return new BaseResponse<UserResponse>
                {
                    Message = "Login Successfull",
                    IsSuccessful = true,
                    Value = new UserResponse
                    {
                        Id = user.Id,
                        Email = user.Email,
                        RoleId = user.RoleId,
                        UserName = user.UserName,
                        WalletId = user.Wallet.Id
                    }
                };
            }
            return new BaseResponse<UserResponse>
            {
               Message = "Invalid Login Credentials",
               IsSuccessful = false 
               
            };
        }
    }
}
