namespace CleanArch.Api.Features.Authentication.Logins;

public static partial class Login
{
    public sealed record Request(string Email, string Password);
}
