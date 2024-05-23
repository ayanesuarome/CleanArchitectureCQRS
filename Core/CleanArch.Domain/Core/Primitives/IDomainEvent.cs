using MediatR;

namespace CleanArch.Domain.Core.Primitives;

/// <summary>
/// Represents the interface for an event that is raised within the domain.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Gets the domain identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the occurred on date and time.
    /// </summary>
    DateTimeOffset OcurredOn { get; }
}
