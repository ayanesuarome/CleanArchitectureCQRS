﻿namespace CleanArch.Contracts.LeaveRequests;

public sealed record CreateLeaveRequestRequest(
    int LeaveTypeId,
    string? RequestComments,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate)
{
}