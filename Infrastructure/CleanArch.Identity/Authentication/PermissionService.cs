using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Identity.Authentication;

internal sealed class PermissionService : IPermissionService
{
    private readonly CleanArchIdentityEFDbContext _dbContext;

    public PermissionService(CleanArchIdentityEFDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
    {
        ICollection<Role>[] roles = await _dbContext.Users
            .Include(user => user.Roles)
            .ThenInclude(user => user.Permissions)
            .Where(user => user.Id == userId)
            .Select(user => user.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(role => role)
            .SelectMany(role => role.Permissions)
            .Select(role => role.Name)
            .ToHashSet();
    }
}
