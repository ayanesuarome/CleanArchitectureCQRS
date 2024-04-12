﻿using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocations;

public static partial class UpdateLeaveAllocation
{
    public sealed record Command() : BaseCommnad, IRequest<Result>
    {
        public int Id { get; set; }
    }
}