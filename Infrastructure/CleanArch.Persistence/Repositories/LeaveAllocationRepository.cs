using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
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

    public async Task<LeaveAllocation> GetEmployeeAllocation(string employeeId, int leaveTypeId)
    {
        return await TableNoTracking
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId);
    }

    public async Task<bool> HasEmployeeAllocation(string employeeId, int leaveTypeId)
    {
        return await TableNoTracking
            .AnyAsync(e => e.EmployeeId == employeeId
                && e.LeaveTypeId == leaveTypeId);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string? employeeId)
    {
        IQueryable<LeaveAllocation> query = TableNoTracking;
        
        if(!string.IsNullOrEmpty(employeeId))
        {
            query = query.Where(e => e.EmployeeId == employeeId);
        }

        return await query
            .Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await TableNoTracking
            .Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}