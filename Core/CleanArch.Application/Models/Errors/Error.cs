namespace CleanArch.Application.Models.Errors;

public class Error(string code, string message)
{
    public readonly string Code = code;
    public readonly string Message = message;

    public static readonly Error None = new(string.Empty, string.Empty);
}
