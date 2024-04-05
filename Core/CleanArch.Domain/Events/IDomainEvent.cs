using MediatR;

namespace CleanArch.Domain.Events;

public interface IDomainEvent : INotification
{
    DateTimeOffset ActionDate { get; }
}
