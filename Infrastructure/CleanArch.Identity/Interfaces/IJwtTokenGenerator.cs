using CleanArch.Identity.Models;

namespace CleanArch.Identity.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(ApplicationUser user);
}
