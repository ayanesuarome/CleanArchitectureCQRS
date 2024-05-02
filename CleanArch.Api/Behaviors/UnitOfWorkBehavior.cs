using CleanArch.Application.Abstractions.Data;
using MediatR;
using System.Transactions;

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
            // when the transaction is disposed and if there is any execption, it rollbacks garanteeing an atomic operation.
            using (TransactionScope transactionScope = new())
            {
                TResponse response = await next();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                transactionScope.Complete();
                return response;
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
