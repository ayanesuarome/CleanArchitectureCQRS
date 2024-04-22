﻿namespace CleanArch.Contracts.LeaveTypes;

public sealed record LeaveTypeListDto
{
    public LeaveTypeListDto(IReadOnlyCollection<LeaveTypeModel> leaveTypes) => LeaveTypes = leaveTypes;

    public IReadOnlyCollection<LeaveTypeModel> LeaveTypes { get; }

    public sealed class LeaveTypeModel(int Id, string Name, int DefaultDays)
    {
    }
}