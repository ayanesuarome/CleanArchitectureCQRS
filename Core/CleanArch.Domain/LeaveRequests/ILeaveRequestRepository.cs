namespace CleanArch.Domain.LeaveRequests;

public interface ILeaveRequestRepository
{
    Task<LeaveRequest> GetByIdAsync(LeaveRequestId id);
    void Add(LeaveRequest entity);
    void Update(LeaveRequest entity);
    void Delete(LeaveRequest entity);
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(LeaveRequestId id);
    Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(Guid? employeeId = null);
}
