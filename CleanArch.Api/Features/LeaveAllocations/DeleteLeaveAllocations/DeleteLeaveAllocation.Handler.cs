using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;

public static partial class DeleteLeaveAllocation
{
    public sealed class Handler(ILeaveAllocationRepository repository)
    : IRequestHandler<Command, Result>
    {
        private readonly ILeaveAllocationRepository _repository = repository;

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(command.Id);

            if (leaveAllocation is null)
            {
                return new NotFoundResult(DomainErrors.LeaveAllocation.NotFound(command.Id));
            }

            await _repository.DeleteAsync(leaveAllocation);

            return new SuccessResult();
        }
    }
}
