using CleanArch.Api.Features.Authentication;
using CleanArch.Application.Extensions;
using CleanArch.Contracts.Identity;
using FluentValidation;

namespace CleanArch.Api.Features.Users.CreateUsers;

public static partial class CreateUser
{
    public sealed class Validator : AbstractValidator<RegistrationRequest>
    {
        public Validator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty()
                .WithError(UserErrors.FirstNameIsRequired());

            RuleFor(m => m.LastName)
                .NotEmpty()
                .WithError(UserErrors.LastNameIsRequired());

            RuleFor(m => m.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithError(UserErrors.EmailIsRequired())
                .EmailAddress()
                    .WithError(UserErrors.EmailIsInvalid());
        }
    }
}
