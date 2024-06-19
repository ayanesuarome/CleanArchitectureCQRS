using CleanArch.Application.Extensions;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.LeaveRequests;
using System.Linq.Expressions;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveRequestRepository : GenericRepository<LeaveRequest, LeaveRequestId>, ILeaveRequestRepository
{
    public LeaveRequestRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<PagedList<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        Guid? employeeId = null)
    {
        IQueryable<LeaveRequest> leaveRequestQuery = TableNoTracking;

        if(employeeId.HasValue)
        {
            leaveRequestQuery = leaveRequestQuery.Where(e => e.RequestingEmployeeId == employeeId && !e.IsCancelled);
        }
        
        if(!string.IsNullOrWhiteSpace(searchTerm))
        {
            leaveRequestQuery = leaveRequestQuery.Where(leave => ((string)leave.LeaveTypeName).Contains(searchTerm));
        }

        leaveRequestQuery = leaveRequestQuery.OrderBy(GetSortProperty(sortColumn), sortOrder);

        return await PagedList<LeaveRequest>.CreateAsync(leaveRequestQuery, page, pageSize);
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
