using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveAllocations;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    internal sealed class Handler(ILeaveAllocationRepository repository) : IQueryHandler<Query, LeaveAllocationDetailsDto>
    {
        private readonly ILeaveAllocationRepository _repository = repository;

        public async Task<Result<LeaveAllocationDetailsDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetLeaveAllocationWithDetails(new LeaveAllocationId(query.Id));

            if (leaveAllocation is null)
            {
                return new NotFoundResult<LeaveAllocationDetailsDto>(DomainErrors.LeaveAllocation.NotFound(query.Id));
            }

            LeaveAllocationDetailsDto dto = new(
                leaveAllocation.NumberOfDays,
                leaveAllocation.Period,
                leaveAllocation.Id);

            return Result.Success<LeaveAllocationDetailsDto>(dto);
        }
    }
}
