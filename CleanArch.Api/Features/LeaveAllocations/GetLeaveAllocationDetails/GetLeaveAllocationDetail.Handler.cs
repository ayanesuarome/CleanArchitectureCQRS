using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    internal sealed class Handler(ILeaveAllocationRepository repository)
        : IQueryHandler<Query, Result<LeaveAllocationDetailsDto>>
    {
        private readonly ILeaveAllocationRepository _repository = repository;

        public async Task<Result<LeaveAllocationDetailsDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetLeaveAllocationWithDetails(query.Id);

            if (leaveAllocation is null)
            {
                return new NotFoundResult<LeaveAllocationDetailsDto>(DomainErrors.LeaveAllocation.NotFound(query.Id));
            }

            LeaveAllocationDetailsDto dto = new(
                leaveAllocation.NumberOfDays,
                leaveAllocation.Period,
                leaveAllocation.Id);

            return new SuccessResult<LeaveAllocationDetailsDto>(dto);
        }
    }
}
