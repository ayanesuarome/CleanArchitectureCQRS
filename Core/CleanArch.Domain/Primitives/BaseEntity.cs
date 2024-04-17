using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Primitives;

public abstract class BaseEntity<T> : IAuditableEntity
{
    //protected BaseEntity(T id)
    //{
    //    Id = id;
    //}

    public T Id { get; set; }
    public DateTimeOffset DateCreated { get; }
    public string CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public string? ModifiedBy { get; }
}
