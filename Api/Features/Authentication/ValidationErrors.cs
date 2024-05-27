using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Api.Features.Authentication;

internal static class ValidationErrors
{
    internal static class CreateUser
    {
        internal static Error FirstNameIsRequired => new("CreateUser.FirstNameIsRequired", "The FirstName is required.");
        internal static Error LastNameIsRequired => new("CreateUser.LastNameIsRequired", "The LastName is required.");
        internal static Error EmailIsRequired => new("CreateUser.EmailIsRequired", "The Email is required.");
        internal static Error EmailIsInvalid => new("CreateUser.EmailIsInvalid", "The Email is invalid.");
        internal static Error CreateUserValidation(string message) => new("CreateUser.Validation", message);
    }

    internal static class Login
    {
        internal static Error EmailIsRequired => new("Login.EmailIsRequired", "The email is required.");
        internal static Error EmailIsInvalid => new("Login.EmailIsInvalid", "The Email is invalid.");
        internal static Error PasswordIsRequired => new("Login.PasswordIsRequired", "The password is required.");
    }
}
