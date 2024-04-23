using CleanArch.Contracts.Identity;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    public sealed record Command(
        string FirstName,
        string LastName,
        string Email,
        string Password)
        : IRequest<Result<RegistrationResponse>>
    {
    }
}
