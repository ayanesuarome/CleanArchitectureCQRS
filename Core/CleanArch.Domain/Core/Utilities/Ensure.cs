using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CleanArch.Domain.Core.Utilities;

public static class Ensure
{
    /// <summary>
    /// Ensures that the specified value is not null.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="message">The message to show if the check fails.</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <exception cref="ArgumentNullException"> if the specified value is null.</exception>
    public static void NotNull<T>(T value, string message, [CallerArgumentExpression("value")] string? argumentName = null)
        where T : class
    {
        if (value is null)
        {
            throw new ArgumentNullException(argumentName, message);
        }
    }

    /// <summary>
    /// Ensures that the specified <see cref="int"/> value is not empty.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="message">The message to show if the check fails.</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <exception cref="ArgumentException"> if the specified value is the default value for the type.</exception>
    public static void NotEmpty<T>(T value, string message, [CallerArgumentExpression("value")] string? argumentName = null)
    {
        Type type = typeof(T);

        if (type == typeof(int))
        {
            int intValue = Convert.ToInt32(value);

            if (intValue == 0)
            {
                throw new ArgumentException(message, argumentName);
            }
        }
        else if (type == typeof(Guid))
        {
            Guid guidValue = Guid.Parse(value.ToString());

            if (guidValue == Guid.Empty)
            {
                throw new ArgumentException(message, argumentName);
            }
        }
    }

    /// <summary>
    /// Ensures that the specified <see cref="DateTimeOffset"/> value is not empty.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="message">The message to show if the check fails.</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <exception cref="ArgumentException"> if the specified value is the default value for the type.</exception>
    public static void NotEmpty(DateTimeOffset value, string message, [CallerArgumentExpression("value")] string? argumentName = null)
    {
        if (value == default)
        {
            throw new ArgumentException(message, argumentName);
        }
    }
}
