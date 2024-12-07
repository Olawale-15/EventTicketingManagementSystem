using EventTicketingApp.Models;

namespace EventTicketingApp.Core.Application.Interfaces.Services
{
    public interface IMailServices
    {
        void SendEMail(EmailDto mailRequest);
        void QRCodeEMail(EmailDto mailRequest, string qrCodeImagePath);
    }
}
