using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TRequest>
    where TResponse : Result
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return next();
    }
}
