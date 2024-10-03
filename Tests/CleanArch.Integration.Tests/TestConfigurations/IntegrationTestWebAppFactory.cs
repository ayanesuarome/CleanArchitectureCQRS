using CleanArch.Identity;
using CleanArch.Integration.Tests.TestConfigurations;
using CleanArch.Persistence;
using CleanArch.Persistence.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Quartz.Impl;
using Testcontainers.MsSql;

namespace CleanArch.Integration.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<CleanArchEFDbContext>));
            services.AddDbContext<CleanArchEFDbContext>((sp, options) =>
            {
                options.UseSqlServer(_dbContainer.GetConnectionString());
                options.AddInterceptors(
                    sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                    sp.GetRequiredService<SoftDeleteEntitiesInterceptor>());
            });

            services.RemoveAll(typeof(DbContextOptions<CleanArchIdentityEFDbContext>));
            services.AddDbContext<CleanArchIdentityEFDbContext>(options => options.UseSqlServer(_dbContainer.GetConnectionString()));

            services.AddUserIdentifierProviderMock();

            var scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Standby();
            scheduler.PauseTrigger(new TriggerKey("ProcessOutboxMessagesJob"));
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}
