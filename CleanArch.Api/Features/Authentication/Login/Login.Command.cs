using CleanArch.Contracts.Identity;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.Authentication.Login;

public static partial class Login
{
    public sealed record Command(string Email, string Password) : IRequest<Result<TokenResponse>>
    {
    }
}
