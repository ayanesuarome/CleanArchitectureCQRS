using CleanArch.Domain.LeaveRequests;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveRequestRepository : GenericRepository<LeaveRequest, LeaveRequestId>, ILeaveRequestRepository
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
            query = query.Where(e => e.RequestingEmployeeId == employeeId && e.IsCancelled == false);
        }
        
        return await query
            .ToArrayAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(LeaveRequestId id)
    {
        return await GetAsNoTrackingByIdAsync(id);
    }
}
