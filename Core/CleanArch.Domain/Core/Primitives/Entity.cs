using CleanArch.Domain.Core.Utilities;
using System.Runtime.CompilerServices;

namespace CleanArch.Domain.Core.Primitives;

/// <summary>
/// Represents the base class that all entities derive from.
/// </summary>
public abstract class Entity<TEntityKey> : IEquatable<Entity<TEntityKey>>
    where TEntityKey : class, IEntityKey
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TKey}"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(TEntityKey id)
    {
        Ensure.NotNull(id, "The identifier is required.", nameof(id));
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

    public static bool operator ==(Entity<TEntityKey>? a, Entity<TEntityKey>? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TEntityKey>? a, Entity<TEntityKey>? b) => !(a == b);

    /// <inheritdoc />
    public virtual bool Equals(Entity<TEntityKey>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is not Entity<TEntityKey> other)
        {
            return false;
        }

        return Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Id.GetHashCode() * 41;
}
