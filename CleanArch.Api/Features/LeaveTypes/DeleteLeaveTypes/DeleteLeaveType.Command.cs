﻿using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.DeleteLeaveTypes;

public static partial class DeleteLeaveType
{
    public sealed record Command(Guid Id) : IRequest<Result<Unit>>
    {
    }
}
