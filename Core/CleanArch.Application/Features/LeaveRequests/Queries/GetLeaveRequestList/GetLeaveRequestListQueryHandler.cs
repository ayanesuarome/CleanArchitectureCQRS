using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository repository, IUserService userService)
    : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveRequestRepository _repository = repository;
    private readonly IUserService _userService = userService;

    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {        
        string userId = _userService.UserId;
        List<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync(userId);
        return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
    }
}
