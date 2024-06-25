using CleanArch.Identity;
using CleanArch.Integration.Tests.TestConfigurations;
using CleanArch.Persistence;
using CleanArch.Persistence.Interceptors;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Quartz.Impl;

namespace CleanArch.Integration.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const string Database = "CleanArch";
    private const string Username = "sa";
    private const string Password = "IntegrationTests123456789^";
    private const ushort MsSqlPort = 1433;

    private readonly IContainer _dbContainer = new ContainerBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPortBinding(MsSqlPort, true)
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithEnvironment("SQLCMDUSER", Username)
        .WithEnvironment("SQLCMDPASSWORD", Password)
        .WithEnvironment("MSSQL_SA_PASSWORD", Password)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MsSqlPort))
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureTestServices(services =>
        {
            string host = _dbContainer.Hostname;
            ushort port = _dbContainer.GetMappedPublicPort(MsSqlPort);
            string connectionString = $"Server={host},{port};Database={Database};User={Username};Password={Password};Trust Server Certificate=True;";

            services.RemoveAll(typeof(DbContextOptions<CleanArchEFWriteDbContext>));            
            services.AddDbContext<CleanArchEFWriteDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(
                    sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                    sp.GetRequiredService<SoftDeleteEntitiesInterceptor>());
            });

            services.RemoveAll(typeof(DbContextOptions<CleanArchEFReadDbContext>));
            services.AddDbContext<CleanArchEFReadDbContext>(options => options.UseSqlServer(connectionString));

            services.RemoveAll(typeof(DbContextOptions<CleanArchIdentityEFDbContext>));
            services.AddDbContext<CleanArchIdentityEFDbContext>(options => options.UseSqlServer(connectionString));

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
