namespace CleanArch.Domain.Interfaces;

/// <summary>
/// Represents soft-deletable entities.
/// </summary>
public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedOn { get; set; }
}
