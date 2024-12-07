namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
