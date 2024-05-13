using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class FirstName
    {
        public static Error NullOrEmpty => new Error("FirstName.NullOrEmpty", "The first name is required.");
        public static Error LongerThanAllowed => new Error("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
    }
}
