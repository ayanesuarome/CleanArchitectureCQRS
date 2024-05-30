using CleanArch.Application.Abstractions.Logging;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Time;
using CleanArch.Persistence;
using CleanArch.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;

namespace CleanArch.Infrastructure.BackgroundJobs;

/// <summary>
/// Represents the background job for processing outbox messages. Scoped lifetime.
/// </summary>
[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly CleanArchEFDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ProcessOutboxMessagesOptions _options;
    private readonly IAppLogger<ProcessOutboxMessagesJob> _logger;
    private readonly ResiliencePipeline _pipeline;

    /// <summary>
    /// Initializes a new instance of the class <see cref="ProcessOutboxMessagesJob"/>.
    /// </summary>
    /// <param name="dbContext">The DB context.</param>
    /// <param name="publisher">The publisher.</param>
    /// <param name="options">The options.</param>
    /// <param name="options">The logger.</param>
    public ProcessOutboxMessagesJob(
        CleanArchEFDbContext dbContext,
        IPublisher publisher,
        IOptions<ProcessOutboxMessagesOptions> options,
        IAppLogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _options = options.Value;
        _logger = logger;
        _pipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions()
                {
                    MaxRetryAttempts = _options.RetryCount,
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(_options.IntervalInSeconds),
                    ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                    OnRetry = retryArguments =>
                    {
                        _logger.LogError(
                            retryArguments.Outcome.Exception,
                            $"Waiting {retryArguments.RetryDelay} before next retry. Current attempt: {retryArguments.AttemptNumber}.");

                        return ValueTask.CompletedTask;
                    }
                })
                .Build();
    }

    public async Task Execute(IJobExecutionContext context)
    {
        IReadOnlyCollection<OutboxMessage> outBoxMessages = await GetOutboxMessages(context.CancellationToken);

        foreach (OutboxMessage message in outBoxMessages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, JsonSerializerSettings);

            if(domainEvent is null)
            {
                _logger.LogWarning("Null Outbox message content for message {OutBoxMessageId}", message.Id);
                continue;
            }

            _logger.LogInformation("Publishing Domain Event: {DomainEventId}", domainEvent.Id);

            PolicyResult policyResult = await _pipeline
                .AsAsyncPolicy()
                .ExecuteAndCaptureAsync(() => _publisher.Publish(domainEvent, context.CancellationToken));

            await UpdateOutboxMessageAsync(message, policyResult.FinalException);

            if(policyResult.Outcome == OutcomeType.Failure)
            {
                _logger.LogError(policyResult.FinalException, "Operation Exception Publishing Domain Event: {DomainEventId}.", domainEvent.Id);
                continue;
            }

            _logger.LogInformation("Processed Domain Event: {DomainEventId}", domainEvent.Id);
        }
    }

    private async Task<IReadOnlyCollection<OutboxMessage>> GetOutboxMessages(CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<OutboxMessage>()
            .Take(_options.BatchSize)
            .ToListAsync(cancellationToken);
    }

    private async Task UpdateOutboxMessageAsync(OutboxMessage message, Exception? exception)
    {
        message.UpdateProcessedOn(SystemTimeProvider.UtcNow);
        message.UpdateError(exception?.ToString());
        await _dbContext.SaveChangesAsync();
    }
}
