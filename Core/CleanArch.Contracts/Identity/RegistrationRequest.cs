namespace CleanArch.Contracts.Identity;

public sealed record RegistrationRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password)
{
}