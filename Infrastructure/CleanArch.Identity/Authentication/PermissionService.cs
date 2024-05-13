using CleanArch.Application.Abstractions.Authentication;

namespace CleanArch.Identity.Authentication;

internal sealed class PermissionService : IPermissionService
{
    private readonly CleanArchIdentityEFDbContext _dbContext;

    public PermissionService(CleanArchIdentityEFDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<HashSet<string>> GetPermissionsAsync(Guid userId)
    {
        HashSet<string> permissions = (from userRole in _dbContext.UserRoles
                  join rolePermission in _dbContext.RolePermissions
                  on userRole.RoleId equals rolePermission.RoleId
                  where userRole.UserId == userId
                  join permission in _dbContext.Permissions
                  on rolePermission.PermissionId equals permission.Id
                  select permission.Name)
                  .ToHashSet();

        return Task.FromResult(permissions);
    }
}
