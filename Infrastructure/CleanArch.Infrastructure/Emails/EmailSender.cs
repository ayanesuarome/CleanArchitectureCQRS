using CleanArch.Application.Abstractions.Email;
using CleanArch.Application.Contracts;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArch.Infrastructure.Emails;

internal sealed class EmailSender : IEmailSender
{
    private readonly EmailOptions _emailOptions;
    private readonly SendGridClient _client;

    public EmailSender(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
        _client = new(_emailOptions.ApiKey);
    }

    public async Task<bool> SendEmail(EmailMessage email)
    {
        SendGridMessage message = new()
        {
            From = new(email: _emailOptions.FromAddress, name: _emailOptions.FromName),
            ReplyTo = new(email: _emailOptions.ReplyTo),
            Subject = email.Subject,
            PlainTextContent = email.Body,
            HtmlContent = email.Body
        };

        //message.AddHeader("Content-Encoding", "gzip");
        message.AddTo(email.To);

        var response = await _client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendEmail(EmailMessageTemplate email)
    {
        EmailAddress to = new(email.To);
        EmailAddress from = new()
        {
            Email = _emailOptions.FromAddress,
            Name = _emailOptions.FromName
        };
        EmailAddress replyTo = new(_emailOptions.ReplyTo);

        SendGridMessage message = MailHelper.CreateSingleTemplateEmail(from, to, email.TemplateId, email.TemplateData);
        message.AddReplyTo(replyTo);

        var response = await _client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
