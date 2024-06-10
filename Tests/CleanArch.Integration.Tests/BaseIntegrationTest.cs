using Bogus;
using CleanArch.Domain.Authentication;
using CleanArch.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

//[assembly: TestCollectionOrderer(TestCollectionPriorityOrderer.TypeName, TestCollectionPriorityOrderer.AssemblyName)]

namespace CleanArch.Integration.Tests;

[TestCaseOrderer(TestCasePriorityOrderer.TypeName, TestCasePriorityOrderer.AssemblyName)]
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;

    protected ISender Sender { get; }
    protected CleanArchEFDbContext DbContext { get; }
    protected UserManager<User> UserManager { get; }
    protected Faker Faker { get; }

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
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
}