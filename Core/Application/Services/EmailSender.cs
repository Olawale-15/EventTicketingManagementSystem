using System;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using EventTicketingApp.Models;
using EventTicketingApp.Core.Application.Interfaces.Services;
using MailKit.Security;
using EventTicketingApp.Models.AttendeeModel;

public class EmailSender : IMailServices
{
    private readonly string smtpServer = "smtp.gmail.com";
    private readonly int smtpPort = 465;
    string username = "ayoolalawal00@gmail.com";
    string password = "hvbu mvpq ptml zltk";
    string senderEmail = "ayooolalawal00@gmail.com";
    public void SendEMail(EmailDto mailRequest)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress("EventTicket", senderEmail));
        message.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        message.Subject = mailRequest.Subject;

        var body = new TextPart("html")
        {
            Text = mailRequest.HtmlContent,
        };
        message.Body = body;

        SmtpClient client = new SmtpClient();
        try
        {
            client.Connect(smtpServer, smtpPort, true);
            client.Authenticate(username, password);
            client.Send(message);
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException("Failed to send email.", ex);
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }

    public void QRCodeEMail(EmailDto mailRequest, string qrCodeImagePath)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("EventTicket", senderEmail));
        message.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        message.Subject = mailRequest.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = mailRequest.HtmlContent;

        var attachment = builder.Attachments.Add(qrCodeImagePath);
        attachment.ContentId = "qrcode";

        message.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect(smtpServer, smtpPort, true);
            client.Authenticate(username, password);
            client.Send(message);
            client.Disconnect(true);
        }
    }

  

}
