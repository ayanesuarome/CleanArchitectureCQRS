using CleanArch.Api.Models;
using CleanArch.Application.Exceptions;
using System.Net;

namespace CleanArch.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        string exceptionMessage = "Oops! Sorry! Something went wrong. Please contact your administrator.";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        dynamic errorDetails;

        //logger.Fatal(context.Exception, errorMessage);

        switch (ex)
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
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = ex.StackTrace
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorDetails);
    }
}
