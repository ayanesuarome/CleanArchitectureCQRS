using CleanArch.Api.Models;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Abstractions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace CleanArch.Api.Middlewares;

// Middleware activated by MiddlewareFactory.
// Use instead the new feature introduced by .NET8, the IExceptionHandler. see folder ExceptionHandlers
public class ExceptionMiddleware(IAppLogger<ExceptionMiddleware> logger) : IMiddleware
{
    private readonly IAppLogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
        Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails errorDetails;

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetails = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Type = nameof(BadRequestException),
                    Detail = badRequestException.InnerException?.Message,
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorDetails = new CustomProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = notFoundException.InnerException?.Message
                };
                break;
            default:
                errorDetails = new CustomProblemDetails
                {
                    Title = "Oops! Sorry! Something went wrong. Please contact your administrator.",
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError)
                    //exception.StackTrace
                };
                break;
        }

        string logMessage = JsonConvert.SerializeObject(errorDetails, Formatting.None);
        _logger.LogError(message: logMessage);

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorDetails);
    }
}
