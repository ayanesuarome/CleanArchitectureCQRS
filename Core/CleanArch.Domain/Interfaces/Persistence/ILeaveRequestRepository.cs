using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string? employeeId = null);
}
