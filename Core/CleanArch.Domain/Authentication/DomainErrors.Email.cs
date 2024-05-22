using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class Email
    {
        public static Error NullOrEmpty => new("Email.NullOrEmpty", "The name is required.");
        public static Error LongerThanAllowed => new("Email.LongerThanAllowed", "The name is longer than allowed.");
        public static Error InvalidFormat => new("Email.InvalidFormat", "The email format is invalid.");
    }
}
