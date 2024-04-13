namespace CleanArch.Domain.Primitives.Result;

public class Error
{
    public Error(string type, string message) : this(type, message, new Dictionary<string, string[]>())
    {
    }

    public Error(string type, string message, IDictionary<string, string[]> errors)
    {
        ArgumentNullException.ThrowIfNull(nameof(type));
        ArgumentNullException.ThrowIfNull(nameof(message));

        Type = type;
        Message = message;
        Errors = errors;
    }

    public string Type { get; }
    public string Message { get; }
    public IDictionary<string, string[]> Errors { get; }

    public static readonly Error None = new(string.Empty, string.Empty);
}
