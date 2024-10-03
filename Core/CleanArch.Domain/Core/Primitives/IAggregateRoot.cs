namespace CleanArch.Domain.Core.Primitives;

/// <summary>
/// Represents the aggregate root interface.
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    /// Clears the list of domain events.
    /// </summary>
    void ClearDomainEvents();

    /// <summary>
    /// Gets the domain events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
}
