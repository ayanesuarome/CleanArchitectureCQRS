using CleanArch.Application.Abstractions.Data;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArch.Api.Behaviors
{
    internal sealed class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        where TResponse : Result
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(IsIdentity())
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
}
