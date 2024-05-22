using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class Comment
    {
        public static Error NullOrEmpty => new("Comment.NullOrEmpty", "The comment is required.");
        public static Error LongerThanAllowed => new("Comment.LongerThanAllowed", "The comment is longer than allowed.");
    }
}
