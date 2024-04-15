using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.Authentication;

internal static class LoginErrors
{
    internal static Error EmailIsRequired => new Error("Login.EmailIsRequired", "The email is required.");
    internal static Error EmailIsInvalid => new Error("User.EmailIsInvalid", "The Email is invalid.");
    internal static Error PasswordIsRequired => new Error("Login.PasswordIsRequired", "The password is required.");
}
