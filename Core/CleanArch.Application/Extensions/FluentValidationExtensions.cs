using CleanArch.Application.ResultPattern;
using FluentValidation;
using FluentValidation.Results;

namespace CleanArch.Application.Extensions;

public static class FluentValidationExtensions
{
    public static ValidationResult AddError(this ValidationResult result, string field, string errorMessage)
    {
        result.Errors.Add(new ValidationFailure(field, errorMessage));
        return result;
    }

    /// <summary>
    /// Specifies a custom error to use if validation fails.
    /// </summary>
    /// <typeparam name="T">The type being validated.</typeparam>
    /// <typeparam name="TProperty">The property being validated.</typeparam>
    /// <param name="rule">The current rule.</param>
    /// <param name="error">The error to use.</param>
    /// <returns>The same rule builder.</returns>
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error)
    {
        return rule
            .WithErrorCode(error.Type)
            .WithMessage(error.Message);
    }
}
