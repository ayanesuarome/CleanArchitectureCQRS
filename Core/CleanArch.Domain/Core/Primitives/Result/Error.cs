namespace CleanArch.Domain.Core.Primitives.Result;

public class Error
{
    public Error(string code, string message)
    {
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(message);

        Code = code;
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }

    public static readonly Error None = new(string.Empty, string.Empty);
}
