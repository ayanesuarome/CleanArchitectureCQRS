using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
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
        return !await TableNoTracking.AnyAsync(t => t.Name.Value == name);
    }

    public async Task<bool> AnyAsync(int id, CancellationToken token = default)
    {
        return await TableNoTracking.AnyAsync(t => t.Id == id);
    }
}
