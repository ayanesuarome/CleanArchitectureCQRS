using System.Runtime.CompilerServices;

namespace CleanArch.Application.Extensions;

public static class ExceptionExtensions
{
    public static bool IsTransient(this Exception exception)
    {
        return exception switch
        {
            // Define transient errors => true,
            _ => false,
        };
    }
}
