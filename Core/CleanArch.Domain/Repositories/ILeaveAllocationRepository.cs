using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ILeaveAllocationRepository
{
    Task<LeaveAllocation> GetByIdAsync(Guid id);
    void Add(LeaveAllocation entity);
    void Update(LeaveAllocation entity);
    void Delete(LeaveAllocation entity);
    void AddRange(IReadOnlyCollection<LeaveAllocation> entities);
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(Guid id);
    Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid? employeeId = null);
    Task<bool> AllocationExists(Guid employeeId, Guid leaveTypeId, int period);
    Task<LeaveAllocation> GetEmployeeAllocation(Guid employeeId, Guid leaveTypeId);
    Task<bool> HasEmployeeAllocation(Guid employeeId, Guid leaveTypeId);
}
