﻿using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;
using CleanArch.Api.Contracts.LeaveRequests;

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
