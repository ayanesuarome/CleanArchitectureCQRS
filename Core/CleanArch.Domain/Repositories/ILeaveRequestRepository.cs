using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
    Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string? employeeId = null);
}
