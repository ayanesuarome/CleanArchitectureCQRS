using CleanArch.Domain.Core.Primitives.Result;
using MediatR;

namespace CleanArch.Application.Abstractions.Messaging;

/// <summary>
/// Marker interface to represent a query with a response.
/// </summary>
/// <typeparam name="TResponse">The query response type.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
