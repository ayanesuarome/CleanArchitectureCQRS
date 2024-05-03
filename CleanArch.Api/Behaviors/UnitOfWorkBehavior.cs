using CleanArch.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArch.Api.Behaviors
{
    internal class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (IsQuery() || IsIdentity())
            {
                return await next();
            }

            // all write operations are made inside the same transaction.
            await using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                TResponse response = await next();

                await transaction.CommitAsync(cancellationToken);

                return response;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        private bool IsQuery()
        {
            if (typeof(TRequest).Name.EndsWith("Query"))
            {
                return true;
            }

            return false;
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
