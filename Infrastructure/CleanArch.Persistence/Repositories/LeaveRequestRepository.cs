using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

internal class LeaveRequestRepository : GenericRepository<LeaveRequest, Guid>, ILeaveRequestRepository
{
    public LeaveRequestRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(Guid? employeeId)
    {
        IQueryable<LeaveRequest> query = TableNoTracking;

        if(employeeId.HasValue)
        {
            query = query.Where(e => e.RequestingEmployeeId == employeeId);
        }
        
        return await query
            .ToArrayAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(Guid id)
    {
        return await GetAsNoTrackingByIdAsync(id);
    }
}
