using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string? employeeId);
    Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period);
    Task<LeaveAllocation> GetEmployeeAllocations(string employeeId, int leaveTypeId);
}
