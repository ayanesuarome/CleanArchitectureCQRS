using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class Name
    {
        public static Error NullOrEmpty => new Error("Name.NullOrEmpty", "The name is required.");
        public static Error LongerThanAllowed => new Error("Name.LongerThanAllowed", "The name is longer than allowed.");
    }
}
