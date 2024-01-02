using CleanArch.Domain.Entities;
using Shouldly;

namespace CleanArch.Persistence.Tests;

public class CleanArchEFDbContextTest(CleanArchEFDbContextFixture fixture)
    : IClassFixture<CleanArchEFDbContextFixture>
{
    private readonly CleanArchEFDbContextFixture _fixture = fixture;

    [Fact]
    public async Task Save_SetDateCreatedAndModifiedValues()
    {
        LeaveType leaveType = new()
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _fixture.context.LeaveTypes.AddAsync(leaveType);
        await _fixture.context.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
        leaveType.DateModified.ShouldNotBeNull();
    }
}