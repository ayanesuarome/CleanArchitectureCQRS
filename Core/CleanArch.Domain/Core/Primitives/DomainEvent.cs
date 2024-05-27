using CleanArch.Domain.Core.Time;

namespace CleanArch.Domain.Core.Primitives;

/// <summary>
/// Represents the abstract domain event primitive.
/// </summary>
public abstract record DomainEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the class <see cref="DomainEvent"/>.
    /// </summary>
    /// <param name="id">The event identifier.</param>
    /// <param name="ocurredOn">The occurred on date and time.</param>
    protected DomainEvent(Guid id, DateTimeOffset ocurredOn)
    {
        Id = id;
        OcurredOn = ocurredOn;
    }

    /// <summary>
    /// Initializes a new instance of the class <see cref="DomainEvent"/>.
    /// </summary>
    protected DomainEvent()
        : this(Guid.NewGuid(), SystemTimeProvider.UtcNow)
    {
    }

    /// <inheritdoc/>
    public Guid Id { get; init; }
    
    /// <inheritdoc/>
    public DateTimeOffset OcurredOn { get; init; }
}
