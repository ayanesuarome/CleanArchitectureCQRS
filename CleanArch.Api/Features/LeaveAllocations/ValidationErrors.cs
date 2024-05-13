using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveAllocations;

internal static class ValidationErrors
{
    internal static class CreateLeaveAllocation
    {
        internal static Error LeaveTypeIdIsRequired => new("CreateLeaveAllocation.LeaveTypeIdIsRequired", "The leave type Id is required.");
        internal static Error CreateLeaveAllocationValidation(string message) => new("CreateLeaveAllocation.Validation", message);
    }

    internal static class UpdateLeaveAllocation
    {
        internal static Error IdIsRequired => new Error("UpdateLeaveAllocation.IdIsRequired", "The Id is required.");
        internal static Error NumberOfDaysGreatherThan(string value) => new(
        "UpdateLeaveAllocation.NumberOfDaysGreatherThan",
        $"The NumberOfDays must be greather than {value}");

        internal static Error PeriodGreaterThanOrEqualToOngoingYear(string period) => new(
            "UpdateLeaveAllocation.PeriodGreaterThanOrEqualToOngoingYear",
            $"Period must be after {period}");

        internal static Error UpdateLeaveAllocationValidation(string message) => new("UpdateLeaveAllocation.Validation", message);
    }

    internal static Error InvalidLeaveAllocation(IDictionary<string, string[]> errors) => new(
        "LeaveAllocation.InvalidLeaveAllocation",
        $"Invalid Leave allocation", errors);
}
