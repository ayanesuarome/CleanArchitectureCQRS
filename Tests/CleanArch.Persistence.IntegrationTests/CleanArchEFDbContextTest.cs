using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.ValueObjects;
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

        LeaveType leaveType = new(name.Value, defaultDays.Value);

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

        LeaveType leaveType = new(name.Value, defaultDays.Value);

        await _fixture.context.LeaveTypes.AddAsync(leaveType);
        await _fixture.context.SaveChangesAsync();

        bool exist = await _fixture.context.LeaveTypes
            .AsNoTracking()
            .AnyAsync(e => e.Id == leaveType.Id);

        exist.ShouldBeTrue();
    }
}