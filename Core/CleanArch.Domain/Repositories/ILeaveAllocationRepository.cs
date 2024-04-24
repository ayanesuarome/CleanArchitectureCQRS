using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(Guid id);
    Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid? employeeId = null);
    Task<bool> AllocationExists(Guid employeeId, Guid leaveTypeId, int period);
    Task<LeaveAllocation> GetEmployeeAllocation(Guid employeeId, Guid leaveTypeId);
    Task<bool> HasEmployeeAllocation(Guid employeeId, Guid leaveTypeId);
}
