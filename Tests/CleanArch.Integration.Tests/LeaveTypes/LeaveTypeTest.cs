using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Api.Features.LeaveTypes.DeleteLeaveTypes;
using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;
using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;
using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;
using FluentAssertions;
using MediatR;

namespace CleanArch.Integration.Tests.LeaveTypes;

//[TestPriority(1)]
public class LeaveTypeTest : BaseIntegrationTest
{
    public LeaveTypeTest(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact, TestPriority(0)]
    public async Task CreateHandlerShouldAdd_NewLeaveTypeToDatabase()
    {
        // Arrange
        CreateLeaveType.Command command = new("Vaca", 10);

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        LeaveType? leaveType = WriteDbContext.LeaveTypes.FirstOrDefault(leave => leave.Id == result.Value);
        Assert.NotNull(leaveType);
    }

    [Fact, TestPriority(1)]
    public async Task UpdateHandlerShouldUpdate_LeaveTypeFromDatabase()
    {
        // Arrange
        LeaveType? leaveType = WriteDbContext.LeaveTypes.FirstOrDefault(leave => leave.Name == "Vaca");
        UpdateLeaveType.Command command = new(leaveType.Id, "Vacation", 20);

        // Act
        Result<Unit> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        leaveType = await WriteDbContext.LeaveTypes.FindAsync(leaveType.Id);
        leaveType.Should().NotBeNull();
        leaveType?.Name.Value.Should().Be("Vacation");
        leaveType?.DefaultDays.Value.Should().Be(20);
    }

    [Fact, TestPriority(3)]
    public async Task GetHandlerShouldFetch_LeaveTypeFromDatabase()
    {
        // Arrange
        LeaveType? leaveType = WriteDbContext.LeaveTypes.FirstOrDefault(leave => leave.Name == "Vacation");
        GetLeaveTypeDetail.Query query = new(leaveType.Id);

        // Act
        Result<GetLeaveTypeDetail.Response> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Name.Should().Be("Vacation");
    }

    [Fact, TestPriority(4)]
    public async Task GetListHandlerShouldFetch_LeaveTypesFromDatabase()
    {
        // Arrange
        Result<Name> nameResult = Name.Create("Sick");
        Result<DefaultDays> defaultDaysResult = DefaultDays.Create(10);
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        Result<LeaveType> leaveTypeResult = LeaveType.Create(nameResult.Value, defaultDaysResult.Value, requirement);

        WriteDbContext.Add(leaveTypeResult.Value);
        await WriteDbContext.SaveChangesAsync();

        GetLeaveTypeList.Query query = new();

        // Act
        Result<GetLeaveTypeList.Response> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.LeaveTypes.Should().NotBeNull();
        result.Value.LeaveTypes.Should().HaveCount(2);
    }

    [Fact, TestPriority(5)]
    public async Task DeleteHandlerShouldDelete_LeaveTypeFromDatabase()
    {
        // Arrange
        LeaveType? leaveType = WriteDbContext.LeaveTypes.FirstOrDefault(leave => leave.Name == "Sick");
        DeleteLeaveType.Command command = new(leaveType.Id);

        // Act
        Result<Unit> result = await Sender.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        leaveType = await WriteDbContext.LeaveTypes.FindAsync(leaveType.Id);
        Assert.Null(leaveType);
    }
}
