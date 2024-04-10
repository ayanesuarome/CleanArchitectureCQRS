using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.DeleteLeaveAllocations;

public static partial class DeleteLeaveAllocation
{
    public class Handler(ILeaveAllocationRepository repository)
    : IRequestHandler<Command, Result>
    {
        private readonly ILeaveAllocationRepository _repository = repository;

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(request.Id);

            if (leaveAllocation is null)
            {
                return new NotFoundResult(LeaveAllocationErrors.NotFound(request.Id));
            }

            await _repository.DeleteAsync(leaveAllocation);

            return new SuccessResult();
        }
    }
}
