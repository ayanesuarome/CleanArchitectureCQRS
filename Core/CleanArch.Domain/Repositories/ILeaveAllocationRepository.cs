using CleanArch.Domain.Entities;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Domain.Repositories;

public interface ILeaveAllocationRepository
{
    Task<LeaveAllocation> GetByIdAsync(LeaveAllocationId id);
    void Add(LeaveAllocation entity);
    void Update(LeaveAllocation entity);
    void Delete(LeaveAllocation entity);
    void AddRange(IReadOnlyCollection<LeaveAllocation> entities);
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(LeaveAllocationId id);
    Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid? employeeId = null);
    Task<bool> AllocationExists(Guid employeeId, LeaveTypeId leaveTypeId, int period);
    Task<LeaveAllocation> GetEmployeeAllocation(Guid employeeId, LeaveTypeId leaveTypeId);
    Task<bool> HasEmployeeAllocation(Guid employeeId, LeaveTypeId leaveTypeId);
}
