namespace CleanArch.Domain.Primitives;

/// <summary>
/// Represents the marker interface for auditable entities.
/// </summary>
public interface IAuditableEntity
{
    public DateTimeOffset DateCreated { get; }
    public Guid CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public Guid? ModifiedBy { get; }
}
