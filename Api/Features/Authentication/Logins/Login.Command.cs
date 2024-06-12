using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Api.Features.Authentication.Logins;

public static partial class Login
{
    public sealed record Command(string Email, string Password) : ICommand<Result<Response>>;
}
