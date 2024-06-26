using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveRequests;

public interface ILeaveRequestSummaryRepository
{
    Task<LeaveRequestSummary> GetByIdAsync(Guid id);
    Task<PagedList<LeaveRequestSummary>> GetLeaveRequestsWithDetailsAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        Guid? employeeId = null);
    void Add(LeaveRequestSummary summary);
    void Update(LeaveRequestSummary summary);
    void Delete(LeaveRequestSummary summary);
}
