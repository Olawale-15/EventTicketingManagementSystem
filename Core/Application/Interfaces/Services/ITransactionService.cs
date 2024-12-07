
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Models;
using EventTicketingApp.Models.TransactionModel;
using System.Linq.Expressions;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<BaseResponse<TransactionResponse>> GetTransactionAsync(Guid id);
        Task<BaseResponse<ICollection<TransactionResponse>>> GetTransactionsByAttendeeId(Guid id);

    }
}
