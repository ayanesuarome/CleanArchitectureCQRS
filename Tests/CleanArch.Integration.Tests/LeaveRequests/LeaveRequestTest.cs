using Bogus;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Domain.Authentication;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveAllocations;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Domain.LeaveTypes;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Integration.Tests.LeaveRequests;

public class LeaveRequestTest : BaseIntegrationTest, IAsyncLifetime
{
    private User Employee { get; set; }
    private LeaveType LeaveType { get; set; }
    private LeaveAllocation LeaveAllocation { get; set; }

    public LeaveRequestTest(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    public async Task InitializeAsync()
    {
        // Asynchronously initialize this instance.
        Employee = await CreateUser();
        LeaveType = await CreateLeaveType();
        LeaveAllocation = await CreateLeaveAllocation(LeaveType, Employee);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    private async Task<User> CreateUser()
    {
        Bogus.DataSets.Name name = Faker.Name;
        Result<UserName> firstNameResult = UserName.Create(name.FirstName());
        Result<UserName> lastNameResult = UserName.Create(name.LastName());
        Result<Email> emailResult = Email.Create(Faker.Internet.Email());

        User user = new(firstNameResult.Value, lastNameResult.Value)
        {
            Id = Guid.Parse("f8b3c041-3397-43f1-95db-6fd3b5eb2e40"),
            Email = emailResult.Value,
            UserName = emailResult.Value,
            EmailConfirmed = true
        };

        IdentityResult result = await UserManager.CreateAsync(user, Faker.Internet.Password(prefix: "%"));
        await UserManager.AddToRoleAsync(user, Roles.Employee.Name);

        return user;
    }

    private async Task<LeaveType> CreateLeaveType()
    {
        Result<Name> nameResult = Name.Create("Vacation");
        Result<DefaultDays> defaultDaysResult = DefaultDays.Create(20);
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        Result<LeaveType> leaveTypeResult = LeaveType.Create(nameResult.Value, defaultDaysResult.Value, requirement);

        DbContext.Add(leaveTypeResult.Value);
        await DbContext.SaveChangesAsync();

        return leaveTypeResult.Value;
    }

    private async Task<LeaveAllocation> CreateLeaveAllocation(LeaveType leaveType, User employee)
    {
        int period = DateTime.Now.Year;

        // assign allocations if an allocation does not already exist for a period and leave type
        LeaveAllocation allocation = new(period, leaveType, employee.Id);
        allocation.ChangeNumberOfDays(leaveType.DefaultDays.Value);
        DbContext.Add(allocation);
        await DbContext.SaveChangesAsync();

        return allocation;
    }

    [Fact, TestPriority(1)]
    public async Task CreateHandlerShouldAdd_NewLeaveRequestTo()
    {
        // Arrange
        string startDate = "06/06/2024";
        string endDate = "06/07/2024";

        CreateLeaveRequest.Command command = new(LeaveType.Id, startDate, endDate, "comments");

        // Act
        Result<LeaveRequest> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        LeaveRequest? leaveRequest = await DbContext.LeaveRequests.FindAsync(result.Value.Id);
        leaveRequest.Should().NotBeNull();
        leaveRequest?.DomainEvents.Should().HaveCount(1).And.AllBeOfType<LeaveRequestCreatedDomainEvent>();
    }
}
