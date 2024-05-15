using CleanArch.Application.Abstractions.Data;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArch.Api.Behaviors;

/// <summary>
/// Represents the transaction behaviour middleware.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
internal sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : Result<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    public TransactionBehavior(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(request is IQuery<Result> || IsIdentity())
        {
            return await next();
        }

        // all write operations are made inside the same transaction.
        await using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            TResponse response = await next();
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync(cancellationToken);
            return response;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }

    private bool IsIdentity()
    {
        if (typeof(TRequest).FullName.Contains("Authentication"))
        {
            return true;
        }

        return false;
    }
}
