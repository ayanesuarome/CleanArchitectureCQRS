using MediatR;

namespace CleanArch.Domain.DomainEvents;

public interface IDomainEvent : INotification
{
    DateTimeOffset ActionDate { get; }
}
