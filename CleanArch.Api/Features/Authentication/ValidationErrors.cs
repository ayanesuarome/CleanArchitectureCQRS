using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.Authentication;

internal static class ValidationErrors
{
    internal static class CreateUser
    {
        internal static Error FirstNameIsRequired => new Error("CreateUser.FirstNameIsRequired", "The FirstName is required.");
        internal static Error LastNameIsRequired => new Error("CreateUser.LastNameIsRequired", "The LastName is required.");
        internal static Error EmailIsRequired => new Error("CreateUser.EmailIsRequired", "The Email is required.");
        internal static Error EmailIsInvalid => new Error("CreateUser.EmailIsInvalid", "The Email is invalid.");
        internal static Error CreateUserValidation(string message) => new Error("CreateUser.Validation", message);
    }

    internal static class Login
    {
        internal static Error EmailIsRequired => new Error("Login.EmailIsRequired", "The email is required.");
        internal static Error EmailIsInvalid => new Error("Login.EmailIsInvalid", "The Email is invalid.");
        internal static Error PasswordIsRequired => new Error("Login.PasswordIsRequired", "The password is required.");
    }
}
