using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveTypeRepository : GenericRepository<LeaveType, LeaveTypeId>, ILeaveTypeRepository
{
    public LeaveTypeRepository(CleanArchEFWriteDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> IsUniqueAsync(Name name, CancellationToken cancellationToken = default)
    {
        var result = !await TableNoTracking.AnyAsync(t => t.Name == name, cancellationToken);
        return result;
    }

    public async Task<bool> AnyAsync(LeaveTypeId id, CancellationToken cancellationToken = default)
    {
        return await TableNoTracking.AnyAsync(t => t.Id == id, cancellationToken);
    }
}
