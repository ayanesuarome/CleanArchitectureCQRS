using CleanArch.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Identity.Authentication;

public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        HashSet<string> permissions = context
            .User
            .FindAll(claim => claim.Type == CustomClaims.Permissions)
            .Select(claim => claim.Value)
            .ToHashSet();

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
