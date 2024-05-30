using CleanArch.Application.Abstractions.Email;
using CleanArch.Application.Abstractions.Logging;
using CleanArch.Application.EventBus;
using CleanArch.Infrastructure.BackgroundJobs;
using CleanArch.Infrastructure.Emails;
using CleanArch.Infrastructure.Events;
using CleanArch.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

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

        return services;
    }
}
