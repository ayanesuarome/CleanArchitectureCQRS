using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveRequests;

public interface ILeaveRequestRepository
{
    Task<LeaveRequest> GetByIdAsync(LeaveRequestId id);
    void Add(LeaveRequest entity);
    void Update(LeaveRequest entity);
    void Delete(LeaveRequest entity);
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(LeaveRequestId id);
    Task<PagedList<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        Guid? employeeId = null);
}
