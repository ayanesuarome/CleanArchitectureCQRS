using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.Authentication;

internal static class UserErrors
{
    internal static Error FirstNameIsRequired => new Error("User.FirstNameIsRequired", "The FirstName is required.");
    internal static Error LastNameIsRequired => new Error("User.LastNameIsRequired", "The LastName is required.");
    internal static Error EmailIsRequired => new Error("User.EmailIsRequired", "The Email is required.");
    internal static Error EmailIsInvalid => new Error("User.EmailIsInvalid", "The Email is invalid.");
    internal static Error CreateUserValidation(string message) => new Error($"CreateUserValidation.Validation", message);
}
