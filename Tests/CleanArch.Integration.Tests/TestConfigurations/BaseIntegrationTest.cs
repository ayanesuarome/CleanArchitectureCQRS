using Bogus;
using CleanArch.Domain.Authentication;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveAllocations;
using CleanArch.Domain.LeaveTypes;
using CleanArch.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//[assembly: TestCollectionOrderer(TestCollectionPriorityOrderer.TypeName, TestCollectionPriorityOrderer.AssemblyName)]

namespace CleanArch.Integration.Tests;

[TestCaseOrderer(TestCasePriorityOrderer.TypeName, TestCasePriorityOrderer.AssemblyName)]
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;

    protected IConfiguration Configuration { get; }

    protected ISender Sender { get; }
    protected CleanArchEFDbContext DbContext { get; }
    protected UserManager<User> UserManager { get; }
    protected Faker Faker { get; }

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Configuration = _scope.ServiceProvider.GetRequiredService<IConfiguration>();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<CleanArchEFDbContext>();
        UserManager = _scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        Faker = new();
    }

    public void Dispose()
    {
        _scope.Dispose();
        DbContext.Dispose();
    }

    protected async Task<User> CreateUser()
    {
        Bogus.DataSets.Name name = Faker.Name;
        Result<UserName> firstNameResult = UserName.Create(name.FirstName());
        Result<UserName> lastNameResult = UserName.Create(name.LastName());
        Result<Email> emailResult = Email.Create(Faker.Internet.Email());

        string? userId = Configuration.GetValue<string>("TestInitialData:UserId");

        User user = new(firstNameResult.Value, lastNameResult.Value)
        {
            Id = Guid.Parse(userId),
            Email = emailResult.Value,
            UserName = emailResult.Value,
            EmailConfirmed = true
        };

        await UserManager.CreateAsync(user, Faker.Internet.Password(prefix: "%"));
        await UserManager.AddToRoleAsync(user, Roles.Employee.Name);

        return user;
    }

    protected async Task<LeaveType> CreateLeaveType()
    {
        string? name = Configuration.GetValue<string>("TestInitialData:LeaveTypeName");
        int defaultDays = Configuration.GetValue<int>("TestInitialData:LeaveTypeDefaultDays");

        Result<Name> nameResult = Name.Create(name);
        Result<DefaultDays> defaultDaysResult = DefaultDays.Create(defaultDays);
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        Result<LeaveType> leaveTypeResult = LeaveType.Create(nameResult.Value, defaultDaysResult.Value, requirement);

        DbContext.Add(leaveTypeResult.Value);
        await DbContext.SaveChangesAsync();

        return leaveTypeResult.Value;
    }

    protected async Task<LeaveAllocation> CreateLeaveAllocation(LeaveType leaveType, User employee)
    {
        int period = DateTime.Now.Year;

        // assign allocations if an allocation does not already exist for a period and leave type
        LeaveAllocation allocation = new(period, leaveType, employee.Id);
        allocation.ChangeNumberOfDays(leaveType.DefaultDays.Value);
        DbContext.Add(allocation);
        await DbContext.SaveChangesAsync();

        return allocation;
    }
}