using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ILeaveRequestRepository
{
    Task<LeaveRequest> GetByIdAsync(Guid id);
    void Add(LeaveRequest entity);
    void Update(LeaveRequest entity);
    void Delete(LeaveRequest entity);
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(Guid id);
    Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(Guid? employeeId = null);
}
