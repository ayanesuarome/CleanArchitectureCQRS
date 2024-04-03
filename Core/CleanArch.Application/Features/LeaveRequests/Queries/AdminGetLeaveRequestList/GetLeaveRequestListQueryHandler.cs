using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.AdminGetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository repository)
    : IRequestHandler<AdminGetLeaveRequestListQuery, List<LeaveRequestDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveRequestRepository _repository = repository;

    public async Task<List<LeaveRequestDto>> Handle(AdminGetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        List<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync();
        return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
    }
}
