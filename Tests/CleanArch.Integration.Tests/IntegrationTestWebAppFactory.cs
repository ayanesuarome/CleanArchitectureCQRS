using CleanArch.Identity;
using CleanArch.Persistence;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        builder.ConfigureTestServices(services =>
        {
            ServiceDescriptor? descriptor = services
                .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<CleanArchEFDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            descriptor = services
                .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<CleanArchIdentityEFDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            var scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Standby();
            scheduler.PauseTrigger(new TriggerKey("ProcessOutboxMessagesJob"));

            string host = _dbContainer.Hostname;
            ushort port = _dbContainer.GetMappedPublicPort(MsSqlPort);
            string connectionString = $"Server={host},{port};Database={Database};User={Username};Password={Password};Trust Server Certificate=True;";

            services.AddDbContext<CleanArchEFDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<CleanArchIdentityEFDbContext>(options => options.UseSqlServer(connectionString));
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
