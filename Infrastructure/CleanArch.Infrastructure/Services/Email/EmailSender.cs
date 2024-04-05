using CleanArch.Application.Abstractions.Email;
using CleanArch.Application.Models.Emails;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArch.Infrastructure.Services.Email;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly SendGridClient _client;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
        _client = new(_emailSettings.ApiKey);
    }

    public async Task<bool> SendEmail(EmailMessage email)
    {
        SendGridMessage message = new()
        {
            From = new(email: _emailSettings.FromAddress, name: _emailSettings.FromName),
            ReplyTo = new(email: _emailSettings.ReplyTo),
            Subject = email.Subject,
            PlainTextContent = email.Body,
            HtmlContent = email.Body
        };

        message.AddHeader("Content-Encoding", "gzip");
        message.AddTo(email.To);

        var response = await _client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendEmail(EmailMessageTemplate email)
    {
        EmailAddress to = new(email.To);
        EmailAddress from = new()
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };
        EmailAddress replyTo = new(_emailSettings.ReplyTo);

        SendGridMessage message = MailHelper.CreateSingleTemplateEmail(from, to, email.TemplateId, email.TemplateData);
        message.AddReplyTo(replyTo);

        var response = await _client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
