﻿using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.DeleteLeaveTypes;

public static partial class DeleteLeaveType
{
    public sealed record Command(int Id) : IRequest<Result<Unit>>
    {
    }
}
