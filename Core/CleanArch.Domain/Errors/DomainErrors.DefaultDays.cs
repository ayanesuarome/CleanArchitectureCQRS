using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class DefaultDays
    {
        public static Error NullOrEmpty => new Error("DefaultDays.NullOrEmpty", "The default days is required.");
        public static Error NotInRange(int min, int max) =>
            new Error("DefaultDays.NotInRange", $"The default days in not in range '{min - max}'.");
    }
}
