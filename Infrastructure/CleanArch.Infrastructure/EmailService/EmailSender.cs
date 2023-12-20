using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArch.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly SendGridClient _client;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
        _client = new(_emailSettings.ApiKey);
    }

    // Another way
    //public async Task<bool> SendEmail(EmailMessage email)
    //{
    //    SendGridClient client = new(_emailSettings.ApiKey);

    //    EmailAddress to = new(email.To);
    //    EmailAddress from = new()
    //    {
    //        Email = _emailSettings.FromAddress,
    //        Name = _emailSettings.FromNa me
    //    };

    //    SendGridMessage message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
    //    var response = await client.SendEmailAsync(message);

    //    return response.IsSuccessStatusCode;
    //}

    public async Task<bool> SendEmail(EmailMessage email)
    {
        SendGridMessage message = new()
        {
            From = new(email: _emailSettings.FromAddress, name: _emailSettings.FromName),
            Subject = email.Subject,
            PlainTextContent = email.Body,
            HtmlContent = email.Body
        };

        message.AddTo(email.To);

        var response = await _client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
