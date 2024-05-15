using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;

namespace CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;

public static partial class DeleteLeaveAllocation
{
    internal sealed class Handler(ILeaveAllocationRepository repository) : ICommandHandler<Command, Result>
    {
        private readonly ILeaveAllocationRepository _repository = repository;

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(command.Id);

            if (leaveAllocation is null)
            {
                return new NotFoundResult(DomainErrors.LeaveAllocation.NotFound(command.Id));
            }

            _repository.Delete(leaveAllocation);

            return Result.Success();
        }
    }
}
