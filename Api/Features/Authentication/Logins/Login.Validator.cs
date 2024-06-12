using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.Authentication.Logins;

public static partial class Login
{
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithError(ValidationErrors.Login.EmailIsRequired)
                .EmailAddress()
                    .WithError(ValidationErrors.Login.EmailIsInvalid);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithError(ValidationErrors.Login.PasswordIsRequired);
        }
    }
}
