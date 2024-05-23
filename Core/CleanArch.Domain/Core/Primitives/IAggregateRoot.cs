namespace CleanArch.Domain.Core.Primitives;

/// <summary>
/// MArker interface.
/// </summary>
public interface IAggregateRoot
{
    void ClearDomainEvents();
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
}
