using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace CleanArch.Persistence.Tests;

public class CleanArchEFDbContextTest(CleanArchEFDbContextFixture fixture) : IClassFixture<CleanArchEFDbContextFixture>
{
    private readonly CleanArchEFDbContextFixture _fixture = fixture;

    [Fact]
    public async Task Save_SetDateCreatedAndModifiedValues()
    {
        Result<Name> name = Name.Create("Test Vacation");
        Result<DefaultDays> defaultDays = DefaultDays.Create(10);
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        LeaveType leaveType = LeaveType.Create(name.Value, defaultDays.Value, requirement).Value;

        await _fixture.context.LeaveTypes.AddAsync(leaveType);
        await _fixture.context.SaveChangesAsync();

        //leaveType.DateCreated.ShouldNotBeNull();
        //leaveType.DateModified.ShouldNotBeNull();
    }

    [Fact]
    public async Task Save_AnyAsync()
    {
        Result<Name> name = Name.Create("Test Get Leave Type");
        Result<DefaultDays> defaultDays = DefaultDays.Create(15);
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        LeaveType leaveType = LeaveType.Create(name.Value, defaultDays.Value, requirement).Value;

        await _fixture.context.LeaveTypes.AddAsync(leaveType);
        await _fixture.context.SaveChangesAsync();

        bool exist = await _fixture.context.LeaveTypes
            .AsNoTracking()
            .AnyAsync(e => e.Id == leaveType.Id);

        exist.ShouldBeTrue();
    }
}