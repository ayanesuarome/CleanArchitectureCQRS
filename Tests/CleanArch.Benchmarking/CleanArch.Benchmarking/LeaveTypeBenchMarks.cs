using BenchmarkDotNet.Attributes;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Benchmarking
{
    [MemoryDiagnoser(false)]
    public class LeaveTypeBenchMarks : EFDbContextInMemory
    {
        [Benchmark]
        public async Task AnyLeaveTypeAsync()
        {
            Result<Name> name = Name.Create("Test Get Leave Type");
            Result<DefaultDays> defaultDays = DefaultDays.Create(15);

            LeaveType entity = new(name.Value, defaultDays.Value);

            await context.LeaveTypes.AddAsync(entity);
            await context.SaveChangesAsync();

            bool exist = await context.LeaveTypes
                .AsNoTracking()
                .AnyAsync(e => e.Id == entity.Id);
        }

        [Benchmark]
        public async Task GetLeaveTypeAsync()
        {
            Result<Name> name = Name.Create("Test Get Leave Type");
            Result<DefaultDays> defaultDays = DefaultDays.Create(15);

            LeaveType entity = new(name.Value, defaultDays.Value);
            
            await context.LeaveTypes.AddAsync(entity);
            await context.SaveChangesAsync();

            LeaveType leaveType = await context.LeaveTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == entity.Id);
        }
    }
}
