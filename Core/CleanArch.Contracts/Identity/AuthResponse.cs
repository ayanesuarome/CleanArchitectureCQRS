namespace CleanArch.Contracts.Identity;

public record AuthResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
