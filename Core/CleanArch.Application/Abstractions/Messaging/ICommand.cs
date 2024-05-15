using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Application.Abstractions.Messaging;

/// <summary>
/// Marker interface to represent a command with a result and a void response.
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// Marker interface to represent a command with a response.
/// </summary>
/// <typeparam name="TResponse">The command response type.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
