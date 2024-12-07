using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Models.WalletModell;
using EventTicketingApp.Models;
using System.Security.Claims;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Infrastructure.Context;

namespace EventTicketingApp.Core.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IEventOrganizerRepository _eventOrganizerRepository;
        private readonly IUserRepository _userRepository;
        public WalletService(IUnitOfWork unitOfWork, IWalletRepository walletRepository, IHttpContextAccessor httpContext, ITransactionRepository transactionRepository, IEventOrganizerRepository eventOrganizerRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _walletRepository = walletRepository;
            _httpContext = httpContext;
            _transactionRepository = transactionRepository;
            _eventOrganizerRepository = eventOrganizerRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<WalletResponse>> GetLoggedInUserWalletAsync()
        {
            var loggedInUserId = _httpContext.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var wallet = await _walletRepository.GetAsync(x => x.UserId.ToString() == loggedInUserId);
            if (wallet == null)
            {
                return new BaseResponse<WalletResponse>
                {
                    Message = "User Wallet Not Found!",
                    IsSuccessful = false
                };
            }

            return new BaseResponse<WalletResponse>
            {
                Message = "User Wallet Found Successfully",
                IsSuccessful = true,
                Value = new WalletResponse
                {
                    Balance = wallet.Balance,
                    Id = wallet.Id,
                    UserId = wallet.UserId
                }
            };
        }

        public async Task<BaseResponse<WalletResponse>> GetWallet(Guid id)
        {
            var wallet = await _walletRepository.GetAsync(id);
            if (wallet == null)
            {
                return new BaseResponse<WalletResponse>
                {
                    Message = "Wallet Not Found",
                    IsSuccessful = false
                };
            }

            return new BaseResponse<WalletResponse>
            {
                Message = "Wallet Found Successfully",
                IsSuccessful = true,
                Value = new WalletResponse
                {
                    Balance = wallet.Balance,
                    Id = wallet.Id,
                    UserId = wallet.UserId
                }
            };
        }

        public async Task<BaseResponse> FundWalletAsync(Guid id, decimal amount)
        {
            var wallet = await _walletRepository.GetAsync(id);
            if (wallet == null)
            {
                return new BaseResponse
                {
                    Message = "User Wallet Not Found",
                    IsSuccessful = false
                };
            }

            var transaction = new Transaction
            {
                Amount = amount,
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Credit,
                Wallet = wallet,
                WalletId = wallet.Id
            };
            //await _transactionRepository.AddAsync(transaction);

            wallet.Balance += amount;
            _walletRepository.UpdateAsync(wallet);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "Wallet Credited Successfully",
                IsSuccessful = true
            };
        }

        public async Task<BaseResponse<WalletResponse>> GetWalletByUserIdAsync(Guid userId)
        {
            var wallet = await _walletRepository.GetAsync(a => a.UserId == userId);
            if (wallet == null)
            {
                return new BaseResponse<WalletResponse>
                {
                    Message = "Wallet Not Found",
                    IsSuccessful = false
                };
            }

            return new BaseResponse<WalletResponse>
            {
                Message = "Wallet Found Successfully",
                IsSuccessful = true,
                Value = new WalletResponse
                {
                    Balance = wallet.Balance,
                    Id = wallet.Id,
                    UserId = wallet.UserId
                }
            };
        }

        public async Task<BaseResponse> DeductFromAdminWalletAsync(decimal amount)
        {
            var adminWallet = await _walletRepository.GetAdminWalletAsync();
            if (adminWallet == null)
            {
                return new BaseResponse
                {
                    IsSuccessful = false,
                    Message = "Admin wallet not found."
                };
            }

            if (adminWallet.Balance < amount)
            {
                return new BaseResponse
                {
                    IsSuccessful = false,
                    Message = "Insufficient funds in admin wallet."
                };
            }

            adminWallet.Balance -= amount;
            await _walletRepository.UpdateAsync(adminWallet);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                IsSuccessful = true,
                Message = "Amount deducted from admin wallet successfully"
            };
        }

        public async Task<BaseResponse> DeductFromOrganizerWalletAsync(Guid eventOrganizerId, decimal amount)
        {
            var organizer = await _eventOrganizerRepository.GetAsync(eventOrganizerId);
            var user = await _userRepository.GetAsync(organizer.UserId);
            var wallet = user.Wallet;
            if (wallet == null)
            {
                return new BaseResponse
                {
                    IsSuccessful = false,
                    Message = "EventOrganizer wallet not found."
                };
            }

            if (wallet.Balance < amount)
            {
                return new BaseResponse
                {
                    IsSuccessful = false,
                    Message = "Insufficient funds in EventOrganizer wallet."
                };
            }
          
            wallet.Balance -= amount;
            await _walletRepository.UpdateAsync(wallet);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                IsSuccessful = true,
                Message = "Amount deducted from Event Organizer wallet successfully"
            };
        }
    }
}
