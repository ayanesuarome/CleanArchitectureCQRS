using CleanArch.Domain.LeaveAllocations;
using CleanArch.Domain.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveAllocationRepository : GenericRepository<LeaveAllocation, LeaveAllocationId>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(CleanArchEFWriteDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> AllocationExists(Guid employeeId, LeaveTypeId leaveTypeId, int period)
    {
        return await TableNoTracking
            .AnyAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId
                && e.Period == period);
    }

    public async Task<LeaveAllocation> GetEmployeeAllocation(Guid employeeId, LeaveTypeId leaveTypeId)
    {
        return await TableNoTracking
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId);
    }

    public async Task<bool> HasEmployeeAllocation(Guid employeeId, LeaveTypeId leaveTypeId)
    {
        return await TableNoTracking
            .AnyAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId);
    }

    public async Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid? employeeId)
    {
        IQueryable<LeaveAllocation> query = TableNoTracking;
        
        if(employeeId.HasValue)
        {
            query = query.Where(e => e.EmployeeId == employeeId);
        }

        return await query
            //.Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(LeaveAllocationId id)
    {
        return await TableNoTracking
            //.Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}