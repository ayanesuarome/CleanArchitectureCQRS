using FluentValidation;

namespace CleanArch.Application.Models.Identity;

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
        
        RuleFor(m => m.UserName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MinimumLength(6)
            .WithMessage("{PropertyName} must contain at least {ComparisonValue} characters");
        
        RuleFor(m => m.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MinimumLength(8)
            .WithMessage("Minimum {PropertyName} allowed is {ComparisonValue}");
    }
}