using System.Collections.Generic;

namespace CleanArch.Domain.Primitives.Result;

public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(IReadOnlyCollection<Error> errors)
        : base(false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public IReadOnlyCollection<Error> Errors { get; }

    public static ValidationResult WithErrors(IReadOnlyCollection<Error> errors) => new(errors);
}

public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(IReadOnlyCollection<Error> errors)
        : base(IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public IReadOnlyCollection<Error> Errors { get; }

    public static ValidationResult<TValue> WithErrors(IReadOnlyCollection<Error> errors) => new(errors);
}