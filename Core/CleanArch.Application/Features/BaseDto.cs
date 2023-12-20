namespace CleanArch.Application.Features;

public abstract class BaseDto
{
    public int Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}
