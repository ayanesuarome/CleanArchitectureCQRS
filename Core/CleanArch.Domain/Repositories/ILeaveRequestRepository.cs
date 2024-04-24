using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(Guid id);
    Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(Guid? employeeId = null);
}
