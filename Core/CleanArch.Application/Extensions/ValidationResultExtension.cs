using FluentValidation.Results;

namespace CleanArch.Application.Extensions;

public static class ValidationResultExtension
{
    public static ValidationResult AddError(this ValidationResult result, string field, string errorMessage)
    {
        result.Errors.Add(new ValidationFailure(field, errorMessage));
        return result;
    }
}
