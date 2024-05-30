namespace CleanArch.Domain.Core.Primitives;

/// <summary>
/// Represents the aggregate root.
/// </summary>
public abstract class AggregateRoot<TEntityKey> : Entity<TEntityKey>, IAggregateRoot
     where TEntityKey : class, IEntityKey
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TEntityKey}"/> class.
    /// </summary>
    /// <param name="id">The aggregate root identifier.</param>
    protected AggregateRoot(TEntityKey id)
        : base(id)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TEntityKey}"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected AggregateRoot()
    {
    }

    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Gets the domain events. This collection is readonly.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Clears all the domain events from the <see cref="AggregateRoot"/>.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Adds the specified <see cref="IDomainEvent"/> to the <see cref="AggregateRoot{TEntityKey}"/>.
    /// </summary>
    /// <param name="domainEvent">The domain event.</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
