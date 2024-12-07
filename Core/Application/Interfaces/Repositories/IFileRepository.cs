namespace EventTicketingApp.Core.Application.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<string> UploadAsync(IFormFile? file);
    }
}
