using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace CleanArch.Persistence.Tests;

public class CleanArchEFDbContextTest(CleanArchEFDbContextFixture fixture) : IClassFixture<CleanArchEFDbContextFixture>
{
    private readonly CleanArchEFDbContextFixture _fixture = fixture;

    [Fact]
    public async Task Save_SetDateCreatedAndModifiedValues()
    {
        LeaveType leaveType = new()
        {
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _fixture.context.LeaveTypes.AddAsync(leaveType);
        await _fixture.context.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
        leaveType.DateModified.ShouldNotBeNull();
    }

    [Fact]
    public async Task Save_AnyAsync()
    {
        LeaveType entity = new()
        {
            DefaultDays = 15,
            Name = "Test Get Leave Type"
        };

        await _fixture.context.LeaveTypes.AddAsync(entity);
        await _fixture.context.SaveChangesAsync();

        bool exist = await _fixture.context.LeaveTypes
            .AsNoTracking()
            .AnyAsync(e => e.Id == entity.Id);

        exist.ShouldBeTrue();
    }
}