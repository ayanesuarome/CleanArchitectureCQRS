namespace CleanArch.Contracts.Identity;

public record AuthRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
