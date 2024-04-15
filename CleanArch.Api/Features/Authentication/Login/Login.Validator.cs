using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.Authentication.Login;

public static partial class Login
{
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithError(AuthenticationErrors.EmailIsRequired)
                .EmailAddress()
                    .WithError(AuthenticationErrors.EmailIsInvalid)

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithError(AuthenticationErrors.PasswordIsRequired);
        }
    }
}
