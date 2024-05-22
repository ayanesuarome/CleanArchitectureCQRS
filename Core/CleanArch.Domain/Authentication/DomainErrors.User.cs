using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class User
    {
        public static Error NotFound(int id) => new("User.NotFound", $"The user with ID '{id}' was not found.");
    }
}
