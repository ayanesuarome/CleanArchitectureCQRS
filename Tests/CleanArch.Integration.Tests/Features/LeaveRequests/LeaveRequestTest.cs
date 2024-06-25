using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;
using CleanArch.Domain.Authentication;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveAllocations;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Domain.LeaveTypes;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArch.Integration.Tests.Features.LeaveRequests;

public class LeaveRequestTest : BaseIntegrationTest
{
    public LeaveRequestTest(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    /// <summary>
    /// Data initialization.
    /// </summary>
    [Fact, TestPriority(0)]
    public async Task PrepareDataAsync()
    {
        // Act
        User employee = await CreateUser();
        LeaveType leaveType = await CreateLeaveType();
        LeaveAllocation allocation = await CreateLeaveAllocation(leaveType, employee);

        // Assert
        allocation.Should().NotBeNull();
    }

    [Fact, TestPriority(1)]
    public async Task CreateHandlerShouldAdd_NewLeaveRequestToDatabase()
    {
        // Arrange
        string startDate = "06/06/2024";
        string endDate = "06/07/2024";

        string? name = Configuration.GetValue<string>("TestInitialData:LeaveTypeName");

        LeaveType? leaveType = WriteDbContext.LeaveTypes.FirstOrDefault(leave => leave.Name == name);

        CreateLeaveRequest.Command command = new(leaveType.Id, startDate, endDate, "comments");

        // Act
        Result<LeaveRequest> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        LeaveRequest? leaveRequest = await WriteDbContext.LeaveRequests.FindAsync(result.Value.Id);
        leaveRequest.Should().NotBeNull();
        leaveRequest?.DomainEvents.Should().HaveCount(1).And.AllBeOfType<LeaveRequestCreatedDomainEvent>();
        leaveRequest?.DateCreated.Should().NotBe(DateTimeOffset.MinValue);
        leaveRequest?.CreatedBy.Should().NotBeEmpty();
    }

    [Fact, TestPriority(2)]
    public async Task DeleteHandlerShouldDelete_LeaveRequestFromDatabase()
    {
        // Arrange
        string? name = Configuration.GetValue<string>("TestInitialData:LeaveTypeName");

        LeaveType? leaveType = WriteDbContext.LeaveTypes.FirstOrDefault(leave => leave.Name == name);
        LeaveRequest? leaveRequest = await WriteDbContext.LeaveRequests.FirstOrDefaultAsync(leave => leave.LeaveTypeId == leaveType.Id);

        DeleteLeaveRequest.Command command = new(leaveRequest.Id);

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        leaveRequest = await WriteDbContext.LeaveRequests
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(leave => leave.Id == leaveRequest.Id);

        leaveRequest.Should().NotBeNull();
        leaveRequest?.IsDeleted.Should().BeTrue();
    }
}
