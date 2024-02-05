namespace CleanArch.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? DateModified { get; set; }
    public string? ModifiedBy { get; set; }
}
