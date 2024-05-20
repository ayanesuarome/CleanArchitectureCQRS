using CleanArch.Domain.Entities;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Domain.Repositories;

public interface ILeaveRequestRepository
{
    Task<LeaveRequest> GetByIdAsync(LeaveRequestId id);
    void Add(LeaveRequest entity);
    void Update(LeaveRequest entity);
    void Delete(LeaveRequest entity);
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(LeaveRequestId id);
    Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(Guid? employeeId = null);
}
