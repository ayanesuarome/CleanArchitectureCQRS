using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string? employeeId)
    {
        IQueryable<LeaveRequest> query = TableNoTracking;

        if(!string.IsNullOrEmpty(employeeId))
        {
            query = query.Where(e => e.RequestingEmployeeId == employeeId);
        }
        
        return await query
            .ToArrayAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
    {
        return await TableNoTracking
            //.Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
