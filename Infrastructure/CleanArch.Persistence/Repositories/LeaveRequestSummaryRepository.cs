using CleanArch.Application.Extensions;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.LeaveRequests;
using System.Linq.Expressions;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveRequestSummaryRepository(CleanArchEFReadDbContext dbContext)
    : ILeaveRequestSummaryRepository
{
    public async Task<LeaveRequestSummary> GetByIdAsync(Guid id)
    {
        return await dbContext.FindAsync<LeaveRequestSummary>(id);
    }

    public async Task<PagedList<LeaveRequestSummary>> GetLeaveRequestsWithDetailsAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        Guid? employeeId = null)
    {
        IQueryable<LeaveRequestSummary> leaveRequestQuery = dbContext.Set<LeaveRequestSummary>();

        if(employeeId.HasValue)
        {
            leaveRequestQuery = leaveRequestQuery.Where(e => e.RequestingEmployeeId == employeeId && !e.IsCancelled);
        }
        
        if(!string.IsNullOrWhiteSpace(searchTerm))
        {
            leaveRequestQuery = leaveRequestQuery.Where(leave => ((string)leave.LeaveTypeName).Contains(searchTerm));
        }

        leaveRequestQuery = leaveRequestQuery.OrderBy(GetSortProperty(sortColumn), sortOrder);

        return await PagedList<LeaveRequestSummary>.CreateAsync(leaveRequestQuery, page, pageSize);
    }

    private static Expression<Func<LeaveRequestSummary, object>> GetSortProperty(string sortColumn) =>
        sortColumn?.ToLower() switch
        {
            "leavetypename" => leave => leave.LeaveTypeName,
            "startdate" => leave => leave.StartDate,
            "enddate" => leave => leave.EndDate,
            "isapproved" => leave => leave.IsApproved,
            _ => leave => leave.Id
        };

    public void Add(LeaveRequestSummary summary)
    {
        dbContext.Add(summary);
    }

    public void Delete(LeaveRequestSummary summary)
    {
        dbContext.Remove(summary);
    }
}
