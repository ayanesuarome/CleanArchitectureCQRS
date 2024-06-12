using CleanArch.Application.Contracts;

namespace CleanArch.Application.Abstractions.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
    Task<bool> SendEmail(EmailMessageTemplate email);
}
