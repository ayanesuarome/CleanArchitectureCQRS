namespace CleanArch.Application.Models.Errors
{
    public class Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
