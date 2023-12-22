using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;

public class GetLeaveAllocationListQueryHandler(IMapper mapper, ILeaveAllocationRepository repository)
    : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveAllocationRepository _repository = repository;

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        // TODO:
        // Get records for specific user
        // Get allocations per employee
        List<LeaveAllocation> leaveAllocations = await _repository.GetLeaveAllocationsWithDetails();
        List<LeaveAllocationDto> leaveAllocationDtos = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        return leaveAllocationDtos;
    }
}
