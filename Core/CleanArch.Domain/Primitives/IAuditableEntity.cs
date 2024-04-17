namespace CleanArch.Domain.Primitives;

/// <summary>
/// Represents the marker interface for auditable entities.
/// </summary>
public interface IAuditableEntity
{
    public DateTimeOffset DateCreated { get; }
    public string CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public string? ModifiedBy { get; }
}
