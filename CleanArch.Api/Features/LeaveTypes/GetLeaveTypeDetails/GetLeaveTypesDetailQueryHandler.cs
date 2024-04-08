﻿using AutoMapper;
using CleanArch.Application.Features.LeaveTypes.Shared;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public class GetLeaveTypesDetailQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    : IRequestHandler<GetLeaveTypeDetailsQuery, Result<LeaveTypeDetailsDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;

    public async Task<Result<LeaveTypeDetailsDto>> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        LeaveType leaveType = await _repository.GetByIdAsync(request.Id);

        if (leaveType is null)
        {
            return new NotFoundResult<LeaveTypeDetailsDto>(LeaveTypeErrors.NotFound(request.Id));
        }

        LeaveTypeDetailsDto dto = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

        return new SuccessResult<LeaveTypeDetailsDto>(dto);
    }
}