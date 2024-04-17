﻿using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class LeaveAllocation
    {
        public static Error NotFound(int id) => new Error("LeaveAllocation.NotFound", $"The leave allocation with Id '{id}' was not found");
        public static Error LeaveTypeMustExist => new Error("LeaveAllocation.LeaveTypeMustExist", "There must be an associated leave type.");

        public static Error InvalidNumberOfDays(int daysRequested) => new Error(
            "LeaveAllocation.InvalidNumberOfDays",
            $"The employee's allocation does not have {daysRequested} available number of days.");
    }
}
