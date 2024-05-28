using System.Runtime.CompilerServices;

namespace CleanArch.Application.Extensions;

public static class ExceptionExtensions
{
    public static bool IsTransient(this Exception exception)
    {
        return exception switch
        {
            ArgumentNullException or InvalidOperationException => false,
            _ => true,
        };
    }
}
