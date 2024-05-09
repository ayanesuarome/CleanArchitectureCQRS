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

    public async Task<HashSet<string>> GetPermissionsAsync(Guid memberId)
    {
        ICollection<Role>[] roles = await _dbContext.Set<User>()
            .Include(user => user.Roles)
            .ThenInclude(user => user.Permissions)
            .Where(user => user.Id == memberId.ToString())
            .Select(user => user.Roles)
            .ToArrayAsync();
        throw new NotImplementedException();
    }
}
