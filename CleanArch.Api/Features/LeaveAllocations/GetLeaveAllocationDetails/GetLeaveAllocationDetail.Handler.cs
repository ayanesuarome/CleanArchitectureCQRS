using AutoMapper;
using CleanArch.Api.Contracts.LeaveAllocations;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    internal sealed class Handler(IMapper mapper, ILeaveAllocationRepository repository)
        : IRequestHandler<Query, Result<LeaveAllocationDetailsDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveAllocationRepository _repository = repository;

        public async Task<Result<LeaveAllocationDetailsDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetLeaveAllocationWithDetails(query.Id);

            if (leaveAllocation is null)
            {
                return new NotFoundResult<LeaveAllocationDetailsDto>(LeaveAllocationErrors.NotFound(query.Id));
            }

            LeaveAllocationDetailsDto dto = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);

            return new SuccessResult<LeaveAllocationDetailsDto>(dto);
        }
    }
}
