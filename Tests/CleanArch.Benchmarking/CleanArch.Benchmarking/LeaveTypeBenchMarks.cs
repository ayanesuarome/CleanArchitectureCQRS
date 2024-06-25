using BenchmarkDotNet.Attributes;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;
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
            LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

            LeaveType entity = LeaveType.Create(name.Value, defaultDays.Value, requirement).Value;

            await Context.LeaveTypes.AddAsync(entity);
            await Context.SaveChangesAsync();

            bool exist = await Context.LeaveTypes
                .AsNoTracking()
                .AnyAsync(e => e.Id == entity.Id);
        }

        [Benchmark]
        public async Task GetLeaveTypeAsync()
        {
            Result<Name> name = Name.Create("Test Get Leave Type");
            Result<DefaultDays> defaultDays = DefaultDays.Create(15);
            LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

            LeaveType entity = LeaveType.Create(name.Value, defaultDays.Value, requirement).Value;
            
            await Context.LeaveTypes.AddAsync(entity);
            await Context.SaveChangesAsync();

            LeaveType? leaveType = await Context.LeaveTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == entity.Id);
        }
    }
}
