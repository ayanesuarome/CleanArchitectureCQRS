using CleanArch.Application.Abstractions.Logging;
using CleanArch.Application.EventBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;

namespace CleanArch.Infrastructure.Events;

internal sealed class IntegrationEventProcessorJob : BackgroundService
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly InMemoryMessageQueue _queue;
    private readonly IPublisher _publisher;
    private readonly IntegrationEventProcessorJobOptions _options;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ResiliencePipeline _pipeline;

    public IntegrationEventProcessorJob(
        InMemoryMessageQueue queue,
        IPublisher publisher,
        IOptions<IntegrationEventProcessorJobOptions> options,
        IServiceScopeFactory serviceScopeFactory)
    {
        _queue = queue;
        _publisher = publisher;
        _options = options.Value;
        _serviceScopeFactory = serviceScopeFactory;

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IAppLogger<IntegrationEventProcessorJob> logger = scope.ServiceProvider
            .GetRequiredService<IAppLogger<IntegrationEventProcessorJob>>();

        _pipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions()
                {
                    MaxRetryAttempts = _options.RetryCount,
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(_options.IntervalInSeconds),
                    ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                    OnRetry = retryArguments =>
                    {
                        logger.LogError(
                            retryArguments.Outcome.Exception,
                            $"Waiting {retryArguments.RetryDelay} before next retry. Current attempt: {retryArguments.AttemptNumber}.");

                        return ValueTask.CompletedTask;
                    }
                })
                .Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IAppLogger<IntegrationEventProcessorJob> logger = scope.ServiceProvider
            .GetRequiredService<IAppLogger<IntegrationEventProcessorJob>>();

        await foreach(IIntegrationEvent integrationEvent in _queue.Reader.ReadAllAsync(stoppingToken))
        {
            logger.LogInformation("Publishing Integration Event: {IntegrationEventId}", integrationEvent.Id);

            PolicyResult policyResult = await _pipeline
            .AsAsyncPolicy()
                .ExecuteAndCaptureAsync(() => _publisher.Publish(integrationEvent, stoppingToken));

            if (policyResult.Outcome == OutcomeType.Failure)
            {
                logger.LogError(policyResult.FinalException, "Operation Exception Publishing Integration Event {IntegrationEventId}", integrationEvent.Id);
                continue;
            }

            logger.LogInformation("Processed Integration Event: {IntegrationEventId}", integrationEvent.Id);
        }
    }
}
