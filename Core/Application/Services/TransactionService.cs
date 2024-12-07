using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Models;
using EventTicketingApp.Models.TransactionModel;

namespace EventTicketingApp.Core.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAttendeeRepository _attendeeRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAttendeeRepository attendeeRepository)
        {
            _attendeeRepository = attendeeRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<BaseResponse<TransactionResponse>> GetTransactionAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetAsync(id);
            if (transaction == null)
            {
                return new BaseResponse<TransactionResponse>
                {
                    Message = "Transaction Not Found",
                    IsSuccessful = false
                };
            }

            return new BaseResponse<TransactionResponse>
            {
                Message = "Transaction Successfully Found",
                IsSuccessful = true,
                Value = new TransactionResponse
                {
                    Amount = transaction.Amount,
                    TransactionDate = transaction.TransactionDate,
                    Type = transaction.Type,
                    WalletId = transaction.WalletId,
                    Id = transaction.Id
                }
            };
        }

        public async Task<BaseResponse<ICollection<TransactionResponse>>> GetTransactionsByAttendeeId(Guid id)
        {
            var attendee = await _attendeeRepository.GetAsync(id);
            if (attendee == null)
            {
                return new BaseResponse<ICollection<TransactionResponse>>
                {
                    Message = "Attendee Not Found",
                    IsSuccessful = false
                };
            }

            var transactions = await _transactionRepository.GetAllUserTransactionAsync(attendee.UserId);

            var transactionResponses = transactions.Select(x => new TransactionResponse
            {
                Id = x.Id,
                Amount = x.Amount,
                TransactionDate = x.TransactionDate,
                Type = x.Type,
                WalletId = x.WalletId
            }).ToList();

            return new BaseResponse<ICollection<TransactionResponse>>
            {
                Message = "List Of Transactions",
                IsSuccessful = true,
                Value = transactionResponses
            };
        }

    }
}
