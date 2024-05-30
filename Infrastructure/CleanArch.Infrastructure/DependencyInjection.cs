using CleanArch.Application.Abstractions.Email;
using CleanArch.Application.Abstractions.Logging;
using CleanArch.Application.EventBus;
using CleanArch.Infrastructure.BackgroundJobs;
using CleanArch.Infrastructure.Emails;
using CleanArch.Infrastructure.Events;
using CleanArch.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using Quartz;

namespace CleanArch.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<EmailOptionsSetup>();
        services.ConfigureOptions<EmailTemplateIdOptionsSetup>();
        services.ConfigureOptions<ProcessOutboxMessagesOptionsSetup>();

        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        

        services.AddResiliencePipeline<string, Task>("publish-domain-event",
            pipelineBuilder =>
            {
                pipelineBuilder
                .AddRetry(new RetryStrategyOptions()
                {
                    MaxRetryAttempts = _options.RetryCount,
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(_options.IntervalInSeconds),
                    ShouldHandle = new PredicateBuilder().Handle<ApplicationException>(),
                    OnRetry = retryArguments =>
                    {
                        _logger.LogError(
                            retryArguments.Outcome.Exception,
                            $"Waiting {retryArguments.RetryDelay} before next retry. Current attempt: {retryArguments.AttemptNumber}.");

                        return ValueTask.CompletedTask;
                    }
                });
            });

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
