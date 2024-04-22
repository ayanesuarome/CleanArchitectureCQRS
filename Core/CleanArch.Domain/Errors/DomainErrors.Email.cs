using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class Email
    {
        public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The name is required.");
        public static Error LongerThanAllowed => new Error("Email.LongerThanAllowed", "The name is longer than allowed.");
        public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.");
    }
}
