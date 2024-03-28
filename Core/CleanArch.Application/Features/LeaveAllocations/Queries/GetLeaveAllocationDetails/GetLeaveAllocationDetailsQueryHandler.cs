using AutoMapper;
using CleanArch.Application.Features.LeaveAllocations.Shared;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository repository)
    : IRequestHandler<GetLeaveAllocationDetailsQuery, Result<LeaveAllocationDetailsDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveAllocationRepository _repository = repository;

    public async Task<Result<LeaveAllocationDetailsDto>> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        LeaveAllocation leaveAllocation = await _repository.GetLeaveAllocationWithDetails(request.Id);

        if(leaveAllocation == null)
        {
            return new NotFoundResult<LeaveAllocationDetailsDto>(LeaveAllocationErrors.NotFound(request.Id));
        }

        LeaveAllocationDetailsDto dto = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);

        return new SuccessResult<LeaveAllocationDetailsDto>(dto);
    }
}
