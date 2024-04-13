using CleanArch.Contracts.Identity;
using FluentValidation;

namespace CleanArch.Api.Features.Users;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(m => m.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(m => m.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(m => m.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .EmailAddress()
            .WithMessage("{PropertyName} invalid");
    }
}