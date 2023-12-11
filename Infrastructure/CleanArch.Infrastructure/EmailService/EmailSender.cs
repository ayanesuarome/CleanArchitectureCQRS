using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArch.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    public readonly EmailSettings emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        this.emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmial(EmailMessage email)
    {
        SendGridClient client = new(emailSettings.ApiKey);
        EmailAddress to = new(email.To);
        EmailAddress from = new()
        {
            Email = emailSettings.FromAddress,
            Name = emailSettings.FromName
        };

        SendGridMessage message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
