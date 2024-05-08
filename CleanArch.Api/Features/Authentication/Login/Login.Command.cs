using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.Authentication.Login;

public static partial class Login
{
    public sealed record Command(string Email, string Password) : ICommand<Result<TokenResponse>>
    {
    }
}
