using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string? employeeId = null);
    Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period);
    Task<LeaveAllocation> GetEmployeeAllocation(string employeeId, int leaveTypeId);
    Task<bool> HasEmployeeAllocation(string employeeId, int leaveTypeId);
}
