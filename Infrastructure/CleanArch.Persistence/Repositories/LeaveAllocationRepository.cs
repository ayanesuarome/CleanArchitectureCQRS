using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using CleanArch.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(CleanArchEFDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
    {
        return await TableNoTracking
            .AnyAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId
                && e.Period == period);
    }

    public async Task<LeaveAllocation> GetEmployeeAllocations(string employeeId, int leaveTypeId)
    {
        return await TableNoTracking
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string? employeeId)
    {
        IQueryable<LeaveAllocation> query = TableNoTracking;
        
        if(!string.IsNullOrEmpty(employeeId))
        {
            query.Where(e => e.EmployeeId == employeeId);
        }

        return await query
            .Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        // TODO: remove if no other logic is used and use GetAsync
        return await TableNoTracking.FirstOrDefaultAsync(e => e.Id == id);
    }
}