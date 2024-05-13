using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class LastName
    {
        public static Error NullOrEmpty => new Error("LastName.NullOrEmpty", "The last name is required.");
        public static Error LongerThanAllowed => new Error("LastName.LongerThanAllowed", "The last name is longer than allowed.");
    }
}
