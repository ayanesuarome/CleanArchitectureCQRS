using CleanArch.Domain.LeaveRequests;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveRequestRepository : GenericRepository<LeaveRequest, LeaveRequestId>, ILeaveRequestRepository
{
    public LeaveRequestRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        Guid? employeeId = null)
    {
        IQueryable<LeaveRequest> query = TableNoTracking;

        if(employeeId.HasValue)
        {
            query = query.Where(e => e.RequestingEmployeeId == employeeId && !e.IsCancelled);
        }
        
        if(!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(leave => leave.LeaveTypeName.Value.Contains(searchTerm));
        }

        if(sortOrder?.ToLower() == "desc")
        {
            query = query.OrderByDescending(GetSortProperty(sortColumn));
        }
        else
        {
            query = query.OrderBy(GetSortProperty(sortColumn));
        }

        return await query
            .ToArrayAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(LeaveRequestId id)
    {
        return await GetAsNoTrackingByIdAsync(id);
    }

    private static Expression<Func<LeaveRequest, object>> GetSortProperty(string sortColumn) => 
        sortColumn?.ToLower() switch
        {
            "leavetypename" => leave => leave.LeaveTypeName,
            "startdate" => leave => leave.Range.StartDate,
            "enddate" => leave => leave.Range.EndDate,
            "isapproved" => leave => leave.IsApproved,
            _ => leave => leave.Id
        };
}
