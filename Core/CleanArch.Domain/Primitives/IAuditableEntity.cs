namespace CleanArch.Domain.Primitives;

public interface IAuditableEntity
{
    public DateTimeOffset? DateCreated { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? DateModified { get; set; }
    public string? ModifiedBy { get; set; }
}
