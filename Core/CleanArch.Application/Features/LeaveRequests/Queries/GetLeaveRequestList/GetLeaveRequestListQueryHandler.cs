using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository repository)
    : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveRequestRepository _repository = repository;

    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        List<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync();
        return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
    }
}
