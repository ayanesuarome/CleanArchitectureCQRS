using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveAllocations;

internal static class LeaveAllocationErrors
{
    internal static Error IdIsRequired => new Error("LeaveAllocation.IdIsRequired", "Id is required.");
    
    internal static Error NumberOfDaysGreatherThan(string value) => new Error(
        "LeaveAllocation.NumberOfDaysGreatherThan",
        $"The NumberOfDays must be greather than {value}");

    internal static Error PeriodGreaterThanOrEqualToOngoingYear(string period) => new Error(
        "LeaveAllocation.PeriodGreaterThanOrEqualToOngoingYear",
        $"Period must be after {period}");
    
    internal static Error InvalidLeaveAllocation(IDictionary<string, string[]> errors) => new Error(
        "LeaveAllocation.InvalidLeaveAllocation",
        $"Invalid Leave allocation", errors);
    
    internal static Error CreateLeaveAllocationValidation(string message) => new Error("LeaveAllocation.CreateLeaveAllocationValidation", message);
    
    internal static Error UpdateLeaveAllocationValidation(string message) => new Error("LeaveAllocation.UpdateLeaveAllocationValidation", message);
}
