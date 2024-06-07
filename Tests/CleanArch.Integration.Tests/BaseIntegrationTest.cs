using CleanArch.Domain.Authentication;
using CleanArch.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

//[assembly: TestCollectionOrderer(TestCollectionPriorityOrderer.TypeName, TestCollectionPriorityOrderer.AssemblyName)]

namespace CleanArch.Integration.Tests;

[TestCaseOrderer(TestCasePriorityOrderer.TypeName, TestCasePriorityOrderer.AssemblyName)]
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly CleanArchEFDbContext DbContext;
    protected readonly UserManager<User> UserManager;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<CleanArchEFDbContext>();
        UserManager = _scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    }
}