﻿using AutoMapper;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;

public class GetLeaveAllocationListQueryHandler(IMapper mapper, ILeaveAllocationRepository repository, IUserService userService)
    : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveAllocationRepository _repository = repository;
    private readonly IUserService _userService = userService;

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        string userId = _userService.UserId;
        List<LeaveAllocation> leaveAllocations = await _repository.GetLeaveAllocationsWithDetails(userId);
        List<LeaveAllocationDto> leaveAllocationDtos = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        return leaveAllocationDtos;
    }
}
