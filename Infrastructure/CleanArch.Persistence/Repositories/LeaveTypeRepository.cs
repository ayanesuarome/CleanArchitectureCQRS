using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using CleanArch.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken token = default)
    {
        return await TableNoTracking.AnyAsync(t => t.Name == name);
    }
}
