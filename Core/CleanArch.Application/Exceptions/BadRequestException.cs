using CleanArch.Domain.Core.Primitives.Result;
using FluentValidation.Results;

namespace CleanArch.Application.Exceptions;

public class BadRequestException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="failures">The collection of validation failures.</param>
    public BadRequestException(IReadOnlyCollection<ValidationFailure> failures)
        : base("One or more validation failures has occurred.") =>
        Errors = failures
            .Distinct()
            .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
            .ToList();

    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    public IReadOnlyCollection<Error> Errors { get; }
}
