using CleanArch.Application.Extensions;
using CleanArch.Contracts.Identity;
using FluentValidation;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty()
                .WithError(ValidationErrors.CreateUser.FirstNameIsRequired);

            RuleFor(m => m.LastName)
                .NotEmpty()
                .WithError(ValidationErrors.CreateUser.LastNameIsRequired);

            RuleFor(m => m.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithError(ValidationErrors.CreateUser.EmailIsRequired)
                .EmailAddress()
                    .WithError(ValidationErrors.CreateUser.EmailIsInvalid);
        }
    }
}
