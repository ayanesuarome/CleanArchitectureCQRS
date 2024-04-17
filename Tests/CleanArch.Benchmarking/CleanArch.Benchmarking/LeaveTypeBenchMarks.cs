using BenchmarkDotNet.Attributes;
using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Benchmarking
{
    [MemoryDiagnoser(false)]
    public class LeaveTypeBenchMarks : EFDbContextInMemory
    {
        [Benchmark]
        public async Task AnyLeaveTypeAsync()
        {
            LeaveType entity = new("Test Get Leave Type", 15)
            {
                Id = 1
            };

            await context.LeaveTypes.AddAsync(entity);
            await context.SaveChangesAsync();

            bool exist = await context.LeaveTypes
                .AsNoTracking()
                .AnyAsync(e => e.Id == entity.Id);
        }

        [Benchmark]
        public async Task GetLeaveTypeAsync()
        {
            LeaveType entity = new("Test Get Leave Type", 15)
            {
                Id = 2
            };
            
            await context.LeaveTypes.AddAsync(entity);
            await context.SaveChangesAsync();

            LeaveType leaveType = await context.LeaveTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == entity.Id);
        }
    }
}
