using CleanArch.Application.Models;

namespace CleanArch.Application.Interfaces.Email;

public interface IEmailSender
{
    Task<bool> SendEmial(EmailMessage email);
}
