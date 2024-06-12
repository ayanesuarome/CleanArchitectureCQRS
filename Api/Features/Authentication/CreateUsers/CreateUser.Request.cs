namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    public sealed record Request(string FirstName, string LastName, string Email, string Password);
}
