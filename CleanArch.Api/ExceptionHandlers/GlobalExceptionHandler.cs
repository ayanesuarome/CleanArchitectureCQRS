using CleanArch.Application.Interfaces.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArch.Api.ExceptionHandlers;

internal sealed class GlobalExceptionHandler(IServiceScopeFactory serviceScopeFactory) : IExceptionHandler
{
    /// <summary>
    /// TryHandleAsync attempts to handle the specified exception within the ASP.NET Core pipeline.
    /// </summary>
    /// <returns>
    /// If the exception can be handled, it should return true.
    /// If the exception can't be handled, it should return false
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IAppLogger<GlobalExceptionHandler> logger = scope
            .ServiceProvider
            .GetRequiredService<IAppLogger<GlobalExceptionHandler>>();

        logger.LogError(exception, "Exception ocurred: {Message}", exception.Message);

        ProblemDetails errorDetails = new()
        {
            Title = "Server error",
            Status = StatusCodes.Status500InternalServerError,
            Type = nameof(HttpStatusCode.InternalServerError)
        };

        httpContext.Response.StatusCode = errorDetails.Status.Value;
        
        await httpContext.Response
            .WriteAsJsonAsync(errorDetails, cancellationToken);

        return true;
    }
}
