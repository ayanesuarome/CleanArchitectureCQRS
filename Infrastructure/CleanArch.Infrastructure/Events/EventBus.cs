using CleanArch.Application.EventBus;

namespace CleanArch.Infrastructure.Events;

/// <summary>
/// Represents the event bus.
/// </summary>
internal sealed class EventBus(InMemoryMessageQueue queue) : IEventBus
{
    /// <inheritdoc />
    public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent
    {
        await queue.Writer.WriteAsync(integrationEvent, cancellationToken);
    }
}
