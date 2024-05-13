using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveAllocations;

internal static class ValidationErrors
{
    internal static class CreateLeaveAllocation
    {
        internal static Error LeaveTypeIdIsRequired => new Error("CreateLeaveAllocation.LeaveTypeIdIsRequired", "The leave type Id is required.");
        internal static Error CreateLeaveAllocationValidation(string message) => new Error("CreateLeaveAllocation.Validation", message);
    }

    internal static class UpdateLeaveAllocation
    {
        internal static Error IdIsRequired => new Error("UpdateLeaveAllocation.IdIsRequired", "The Id is required.");
        internal static Error NumberOfDaysGreatherThan(string value) => new Error(
        "UpdateLeaveAllocation.NumberOfDaysGreatherThan",
        $"The NumberOfDays must be greather than {value}");

        internal static Error PeriodGreaterThanOrEqualToOngoingYear(string period) => new Error(
            "UpdateLeaveAllocation.PeriodGreaterThanOrEqualToOngoingYear",
            $"Period must be after {period}");

        internal static Error UpdateLeaveAllocationValidation(string message) => new Error("UpdateLeaveAllocation.Validation", message);
    }

    internal static Error InvalidLeaveAllocation(IDictionary<string, string[]> errors) => new Error(
        "LeaveAllocation.InvalidLeaveAllocation",
        $"Invalid Leave allocation", errors);
}
