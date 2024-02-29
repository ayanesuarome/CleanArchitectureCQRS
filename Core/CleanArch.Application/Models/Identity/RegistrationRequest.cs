namespace CleanArch.Application.Models.Identity;

public record RegistrationRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password  { get; set; }
}