using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using CleanArch.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string? employeeId)
    {
        IQueryable<LeaveRequest> query = TableNoTracking;

        if(!string.IsNullOrEmpty(employeeId))
        {
            query = query.Where(e => e.RequestingEmployeeId == employeeId && e.IsCancelled == false);
        }
        
        return await query
            .Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
    {
        return await TableNoTracking
            .Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
