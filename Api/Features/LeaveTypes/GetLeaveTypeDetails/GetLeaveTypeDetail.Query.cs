﻿using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    public sealed record Query(Guid Id) : IQuery<Response>;
}
