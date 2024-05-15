using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.Identity;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    public sealed record Command(
        string FirstName,
        string LastName,
        string Email,
        string Password)
        : ICommand<RegistrationResponse>
    {
    }
}
