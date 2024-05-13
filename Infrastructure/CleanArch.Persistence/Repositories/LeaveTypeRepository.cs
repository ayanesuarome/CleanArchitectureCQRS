using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveTypeRepository : GenericRepository<LeaveType, Guid>, ILeaveTypeRepository
{
    public LeaveTypeRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken token = default)
    {
        return !await TableNoTracking.AnyAsync(t => t.Name.Value == name);
    }

    public async Task<bool> AnyAsync(Guid id, CancellationToken token = default)
    {
        return await TableNoTracking.AnyAsync(t => t.Id == id);
    }
}
