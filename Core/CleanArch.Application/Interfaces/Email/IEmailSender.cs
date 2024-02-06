using CleanArch.Application.Models.Emails;

namespace CleanArch.Application.Interfaces.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
    Task<bool> SendEmail(EmailMessageTemplate email);
}
