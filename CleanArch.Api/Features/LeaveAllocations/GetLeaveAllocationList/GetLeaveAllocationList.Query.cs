﻿using CleanArch.Api.Contracts.LeaveAllocations;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    public sealed record Query : IRequest<Result<LeaveAllocationListDto>>
    {
    }
}