using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    public sealed record Command(
        string FirstName,
        string LastName,
        string Email,
        string Password)
        : ICommand<Result<RegistrationResponse>>;
}
