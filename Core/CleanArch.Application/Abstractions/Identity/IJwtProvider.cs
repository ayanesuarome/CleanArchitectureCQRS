using CleanArch.Domain.Entities;

namespace CleanArch.Application.Abstractions.Identity;

public interface IJwtProvider
{
    /// <summary>
    /// Creates the JWT for the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The JWT for the specified user.</returns>
    Task<string> GenerateToken(User user);
}
