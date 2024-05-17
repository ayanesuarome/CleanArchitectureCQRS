using CleanArch.Domain.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Infrastructure;

[Route("api/v{version:apiVersion}")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected ISender Sender { get; }
    protected IPublisher Publisher { get; }

    protected BaseController(ISender sender, IPublisher publisher)
    {
        Sender = sender;
        Publisher = publisher;
    }

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(
                $"You cannot handle a failure result when {nameof(result.IsSuccess)} is true"),
            IValidationResult validationResult => BadRequest(result.Error, validationResult.Errors),
            INotFoundResult => NotFound(),
            _ => BadRequest(result.Error),
        };

    /// <summary>
    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
    /// response based on the specified <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
    protected IActionResult BadRequest(Error error)
    {
        ProblemDetails problemDetails = CreateProblemDetails(
            "Bad Request",
            StatusCodes.Status400BadRequest,
            error);

        return base.BadRequest(problemDetails);
    }

    /// <summary>
    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
    /// response based on the specified <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <param name="errors">The validation errors.</param>
    /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
    protected IActionResult BadRequest(Error error, IReadOnlyCollection<Error> errors)
    {
        ProblemDetails problemDetails = CreateProblemDetails(
            "Validation Error",
            StatusCodes.Status400BadRequest,
            error,
            errors);

        return base.BadRequest(problemDetails);
    }

    /// <summary>
    /// Creates an <see cref="NotFoundResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/>.
    /// </summary>
    /// <returns>The created <see cref="NotFoundResult"/> for the response.</returns>
    protected new IActionResult NotFound() => base.NotFound();
    protected new IActionResult NoContent() => base.NoContent();

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        IReadOnlyCollection<Error>? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
