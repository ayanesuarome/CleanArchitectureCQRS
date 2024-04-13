namespace CleanArch.Domain.Primitives;

public abstract class BaseEntity<T> : IAuditableEntity
{
    public T Id { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? DateModified { get; set; }
    public string? ModifiedBy { get; set; }
}
