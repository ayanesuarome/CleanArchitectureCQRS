using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveTypeRepository : GenericRepository<LeaveType, LeaveTypeId>, ILeaveTypeRepository
{
    public LeaveTypeRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> IsUniqueAsync(Name name, CancellationToken token = default)
    {
        return !await TableNoTracking.AnyAsync(t => t.Name == name);
    }

    public async Task<bool> AnyAsync(LeaveTypeId id, CancellationToken token = default)
    {
        return await TableNoTracking.AnyAsync(t => t.Id == id);
    }
}
