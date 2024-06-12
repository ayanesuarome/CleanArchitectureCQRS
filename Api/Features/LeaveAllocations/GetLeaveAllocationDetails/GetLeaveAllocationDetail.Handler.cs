using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveAllocations;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    internal sealed class Handler(ILeaveAllocationRepository repository) : IQueryHandler<Query, Response>
    {
        private readonly ILeaveAllocationRepository _repository = repository;

        public async Task<Result<Response>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetLeaveAllocationWithDetails(new LeaveAllocationId(query.Id));

            if (leaveAllocation is null)
            {
                return new NotFoundResult<Response>(DomainErrors.LeaveAllocation.NotFound(query.Id));
            }

            Response dto = new(
                leaveAllocation.NumberOfDays,
                leaveAllocation.Period,
                leaveAllocation.Id);

            return Result.Success<Response>(dto);
        }
    }
}
