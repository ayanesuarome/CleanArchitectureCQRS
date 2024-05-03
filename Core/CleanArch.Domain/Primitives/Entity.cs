using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Primitives;

/// <summary>
/// Represents the base class that all entities derive from.
/// </summary>
public abstract class Entity<TEntityKey> : IEquatable<Entity<TEntityKey>>
    where TEntityKey : new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TKey}"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(TEntityKey id)
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TKey}"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Entity()
    {
    }

    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public TEntityKey Id { get; init; }

    /// <inheritdoc />
    public bool Equals(Entity<TEntityKey>? other)
    {
        if(other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other)
            || Id.ToString() == other.Id.ToString();
    }
}
