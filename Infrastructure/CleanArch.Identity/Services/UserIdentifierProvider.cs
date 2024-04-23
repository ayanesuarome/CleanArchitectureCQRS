using CleanArch.Application.Abstractions.Identity;
using Microsoft.AspNetCore.Http;

namespace CleanArch.Identity.Services;

public class UserIdentifierProvider : IUserIdentifierProvider
{
    public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
    {
        string userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value
            ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

        UserId = new Guid(userIdClaim);
    }

    public Guid UserId { get; }
}
