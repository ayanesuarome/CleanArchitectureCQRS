using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string? employeeId);
}
