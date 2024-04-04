namespace CleanArch.Domain.Primitives;

/// <summary>
/// Represents soft-deletable entities.
/// </summary>
public interface ISoftDeletableEntity
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedOn { get; set; }
}
