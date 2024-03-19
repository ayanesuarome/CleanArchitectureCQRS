using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.AdminGetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository repository)
    : IRequestHandler<AdminGetLeaveRequestListQuery, Result<List<LeaveRequestDto>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveRequestRepository _repository = repository;

    public async Task<Result<List<LeaveRequestDto>>> Handle(AdminGetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        List<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync();
        List<LeaveRequestDto> dtos = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

        return Result<List<LeaveRequestDto>>.Success(dtos);
    }
}
