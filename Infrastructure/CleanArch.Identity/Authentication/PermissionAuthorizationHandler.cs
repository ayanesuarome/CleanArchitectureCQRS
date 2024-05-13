using CleanArch.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CleanArch.Identity.Authentication;

public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        string? userId = context.User.FindFirstValue("uid");

        if(!Guid.TryParse(userId, out Guid id))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IPermissionService permissionService = scope.ServiceProvider
            .GetRequiredService<IPermissionService>();

        HashSet<string> permissions = await permissionService.GetPermissionsAsync(id);

        if(permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
