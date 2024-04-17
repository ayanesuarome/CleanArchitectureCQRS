namespace CleanArch.Domain.Primitives;

/// <summary>
/// Represents the marker interface for soft-deletable entities.
/// </summary>
public interface ISoftDeletableEntity
{
    bool IsDeleted { get; }
    DateTimeOffset? DeletedOn { get; }
}
