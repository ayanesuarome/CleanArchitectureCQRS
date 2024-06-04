using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.Time;
using MediatR;
using Serilog.Context;

namespace CleanArch.Api.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Processing request {RequestName}, {DateTimeUtc}",
            requestName,
            SystemTimeProvider.UtcNow.DateTime);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            _logger.LogInformation("Completed request {RequestName}, {DateTimeUtc}",
            requestName,
            SystemTimeProvider.UtcNow.DateTime);

            return result;
        }

        if (result is IValidationResult validationResult)
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            using (LogContext.PushProperty("ValidationError", validationResult.Errors, true))
            {
                _logger.LogError("Completed request {RequestName}, {DateTimeUtc} with error",
                    requestName,
                    SystemTimeProvider.UtcNow.DateTime);
            }
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                _logger.LogError("Completed request {RequestName}, {DateTimeUtc} with error",
                    requestName,
                    SystemTimeProvider.UtcNow.DateTime);
            }
        }

        return result;
    }
}
