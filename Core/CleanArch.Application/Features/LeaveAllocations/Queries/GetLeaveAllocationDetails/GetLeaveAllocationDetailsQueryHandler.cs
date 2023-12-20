using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository repository)
    : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveAllocationRepository _repository = repository;

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        LeaveAllocation leaveAllocation = await _repository.GetLeaveAllocationWithDetails(request.Id);

        if(leaveAllocation == null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);

    }
}
