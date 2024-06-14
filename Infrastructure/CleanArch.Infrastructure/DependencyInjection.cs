using CleanArch.Application.Abstractions.Email;
using CleanArch.Application.EventBus;
using CleanArch.Infrastructure.BackgroundJobs;
using CleanArch.Infrastructure.Emails;
using CleanArch.Infrastructure.Events;
using CleanArch.Infrastructure.Health;
using CleanArch.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Quartz;

namespace CleanArch.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<EmailOptionsSetup>();
        services.ConfigureOptions<EmailTemplateIdOptionsSetup>();
        services.ConfigureOptions<ProcessOutboxMessagesOptionsSetup>();
        services.ConfigureOptions<IntegrationEventProcessorJobOptionsSetup>();

        services.AddTransient<IEmailSender, EmailSender>();

        services.AddQuartz(configure =>
        {
            JobKey jobKey = new(nameof(ProcessOutboxMessagesJob));

            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                {
                    trigger
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule
                                .WithIntervalInSeconds(20)
                                .RepeatForever());
                });
        });

        services.AddQuartzHostedService();

        services.AddSingleton<InMemoryMessageQueue>();
        services.AddSingleton<IEventBus, EventBus>();
        services.AddHostedService<IntegrationEventProcessorJob>();

        string sqlServerConnectionString = configuration.GetConnectionString(CleanArchEFDbContext.ConnectionStringName)!;

        services.AddHealthChecks()
            // only use custom health checks when required
            .AddCheck<CustomDatabaseHealthCheck>("custom-sql", HealthStatus.Unhealthy)
            // using AspNetCore.HealthChecks.SqlServer
            .AddSqlServer(sqlServerConnectionString)
            // using DB context. Slower response than using AspNetCore.HealthChecks.SqlServer and less informative when server is inaccessible
            .AddDbContextCheck<CleanArchEFDbContext>();

        return services;
    }
}
