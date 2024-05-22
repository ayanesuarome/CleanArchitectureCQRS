using System.Collections.Generic;

namespace CleanArch.Domain.Core.Primitives.Result;

public interface IValidationResult
{
    public static Error ValidationError => new("ValidationError", "A validation problem occurred.");

    IReadOnlyCollection<Error> Errors { get; }
}

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