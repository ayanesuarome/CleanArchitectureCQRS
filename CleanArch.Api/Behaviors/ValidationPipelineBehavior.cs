using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;
using FluentValidation;
using MediatR;

namespace CleanArch.Api.Behaviors;

/// <summary>
/// Represents the validation behaviour middleware.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
internal sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationPipelineBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="validators">The validator for the current request type.</param>
    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(!_validators.Any())
        {
            return await next();
        }

        IReadOnlyCollection<Error> errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                failure.ErrorCode, // failure.PropertyName or failure.ErrorCode depending on the design
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if(!errors.Any())
        {
            return await next();
        }

        return CreateValidationResult<TResponse>(errors);
    }

    private static TResult CreateValidationResult<TResult>(IReadOnlyCollection<Error> errors)
        where TResult : class
    {
        if(typeof(TResult) == (typeof(Result)))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object[] { errors })!;

        return (TResult)validationResult;
    }
}
