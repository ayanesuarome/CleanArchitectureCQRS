using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository repository, IUserService userService)
    : IRequestHandler<GetLeaveRequestListQuery, Result<List<LeaveRequestDto>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveRequestRepository _repository = repository;
    private readonly IUserService _userService = userService;

    public async Task<Result<List<LeaveRequestDto>>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {        
        string userId = _userService.UserId;
        List<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync(userId);
        List<LeaveRequestDto> dtos = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

        return new SuccessResult<List<LeaveRequestDto>>(dtos);
    }
}
