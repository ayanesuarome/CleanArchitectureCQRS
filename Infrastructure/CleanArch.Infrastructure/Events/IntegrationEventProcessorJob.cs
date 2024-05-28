﻿using CleanArch.Application.Abstractions.Logging;
using CleanArch.Application.EventBus;
using CleanArch.Application.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanArch.Infrastructure.Events;

internal sealed class IntegrationEventProcessorJob(
    InMemoryMessageQueue queue,
    IPublisher publisher,
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private int RetryCount => 3;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IAppLogger<IntegrationEventProcessorJob> logger = scope.ServiceProvider
            .GetRequiredService<IAppLogger<IntegrationEventProcessorJob>>();

        await foreach(IIntegrationEvent integrationEvent in queue.Reader.ReadAllAsync(stoppingToken))
        {
            logger.LogInformation("Publishing {IntegrationEventId}", integrationEvent.Id);

            int currentRetry = 0;

            for (; ; )
            {
                try
                {
                    await publisher.Publish(integrationEvent, stoppingToken);
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Operation Exception Publishing {IntegrationEventId}", integrationEvent.Id);
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
            
            logger.LogInformation("Processed {IntegrationEventId}", integrationEvent.Id);
        }
    }
}
