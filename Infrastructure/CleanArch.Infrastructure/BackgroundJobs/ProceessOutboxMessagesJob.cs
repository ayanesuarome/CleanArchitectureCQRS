using CleanArch.Application.Abstractions.Logging;
using CleanArch.Application.Extensions;
using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Time;
using CleanArch.Persistence;
using CleanArch.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace CleanArch.Infrastructure.BackgroundJobs;

/// <summary>
/// Scoped lifetime.
/// </summary>
[DisallowConcurrentExecution]
public sealed class ProceessOutboxMessagesJob(
    CleanArchEFDbContext dbContext,
    IPublisher publisher,
    IAppLogger<ProceessOutboxMessagesJob> logger) : IJob
{
    private int RetryCount => 3;

    public async Task Execute(IJobExecutionContext context)
    {
        var outBoxMessages = await dbContext
            .Set<OutboxMessage>()
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage message in outBoxMessages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content);

            if(domainEvent is null)
            {
                logger.LogWarning("Null Outbox message content for message {OutBoxMessageId}", message.Id);
                continue;
            }

            logger.LogInformation("Publishing {DomainEventId}", domainEvent.Id);

            int currentRetry = 0;

            for (; ; )
            {
                try
                {
                    await publisher.Publish(domainEvent, context.CancellationToken);
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Operation Exception Publishing {DomainEventId}", domainEvent.Id);
                    currentRetry++;

                    // Check if the exception thrown was a transient exception based on the logic in the error detection strategy.
                    // Determine whether to retry the operation, as well as how long to wait, based on the retry strategy.
                    if (currentRetry > RetryCount || !ex.IsTransient())
                    {
                        // If this is not a transient error or we should not retry re-throw the exception.
                        throw;
                    }
                }

                // Wait to retry the operation.
                // Consider calculating an exponential delay here and using a strategy best suited for the operation and fault.
                await Task.Delay(10);
            }

            message.ProcessedOn = SystemTimeProvider.UtcNow;

            logger.LogInformation("Processed {IntegrationEventId}", domainEvent.Id);
        }

        if (outBoxMessages.Any())
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
