using CleanArch.Domain.Core.Utilities;
using System.Runtime.CompilerServices;

namespace CleanArch.Domain.Core.Primitives;

/// <summary>
/// Represents the base class that all entities derive from.
/// </summary>
public abstract class Entity<TEntityKey>
    where TEntityKey : class, IEquatable<TEntityKey>
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
}
