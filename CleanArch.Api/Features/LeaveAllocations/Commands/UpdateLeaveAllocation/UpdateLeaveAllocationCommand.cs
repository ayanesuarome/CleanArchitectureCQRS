﻿using CleanArch.Application.Features.LeaveAllocations.Shared;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public record UpdateLeaveAllocationCommand : BaseLeaveAllocationCommnad, IRequest<Result>
{
    public int Id { get; set; }
}