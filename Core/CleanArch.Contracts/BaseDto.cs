namespace CleanArch.Contracts;

public abstract record BaseDto
{
    public int Id { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
    public DateTimeOffset? DateModified { get; set; }
}
