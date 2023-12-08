using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using CleanArch.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string? employeeId)
    {
        IQueryable<LeaveRequest> query = TableNoTracking;

        if(!string.IsNullOrEmpty(employeeId))
        {
            query = query.Where(e => e.RequestingEmployeeId == employeeId);
        }
        
        return await query
            .Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        return await TableNoTracking
            .Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
