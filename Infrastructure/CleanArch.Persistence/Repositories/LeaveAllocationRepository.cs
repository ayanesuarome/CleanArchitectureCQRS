using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

internal class LeaveAllocationRepository : GenericRepository<LeaveAllocation, Guid>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> AllocationExists(Guid employeeId, Guid leaveTypeId, int period)
    {
        return await TableNoTracking
            .AnyAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId
                && e.Period == period);
    }

    public async Task<LeaveAllocation> GetEmployeeAllocation(Guid employeeId, Guid leaveTypeId)
    {
        return await TableNoTracking
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId);
    }

    public async Task<bool> HasEmployeeAllocation(Guid employeeId, Guid leaveTypeId)
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

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(Guid id)
    {
        return await TableNoTracking
            //.Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}