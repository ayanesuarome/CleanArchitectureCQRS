using CleanArch.Domain.Core.Primitives;
using MediatR;

namespace CleanArch.Application.Abstractions.Messaging;

/// <summary>
/// Represents the domain event handler interface.
/// </summary>
/// <typeparam name="TDomainEvent">The domain event type.</typeparam>
public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}
