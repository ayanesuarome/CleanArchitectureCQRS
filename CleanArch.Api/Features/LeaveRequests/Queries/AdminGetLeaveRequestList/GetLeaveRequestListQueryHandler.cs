﻿using AutoMapper;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
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

        return new SuccessResult<List<LeaveRequestDto>>(dtos);
    }
}
