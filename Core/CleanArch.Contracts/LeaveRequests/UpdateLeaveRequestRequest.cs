﻿namespace CleanArch.Contracts.LeaveRequests
{
    public sealed record UpdateLeaveRequestRequest(
        string? RequestComments,
        bool IsCancelled,
        DateTimeOffset StartDate,
        DateTimeOffset EndDate)
    {
    }
}