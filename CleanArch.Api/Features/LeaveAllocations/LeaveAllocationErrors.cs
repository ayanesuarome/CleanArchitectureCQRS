using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveAllocations;

public static class LeaveAllocationErrors
{
    public static Error IdRequired() => new Error("LeaveAllocation.IdRequired", $"{nameof(LeaveAllocation.Id)} is required.");
    public static Error NumberOfDaysGreatherThan(string value) => new Error("LeaveAllocation.NumberOfDaysGreatherThan", $"The {nameof(LeaveAllocation.NumberOfDays)} must be greather than {value}");
    public static Error PeriodGreaterThanOrEqualToOngoingYear(string period) => new Error("LeaveAllocation.PeriodGreaterThanOrEqualToOngoingYear", $"{nameof(LeaveAllocation.Period)} must be after {period}");
    public static Error LeaveTypeMustExist() => new Error("LeaveAllocation.LeaveTypeMustExist", "There must be an associated Leave type.");
    public static Error NotFound(int id) => new Error("LeaveAllocation.NotFound", $"Leave allocation with Id '{id}' not found");
    public static Error InvalidLeaveAllocation(IDictionary<string, string[]> errors) => new Error("LeaveAllocation.InvalidLeaveAllocation", $"Invalid Leave allocation", errors);
    public static Error CreateLeaveAllocationValidation(string message) => new Error("LeaveAllocation.CreateLeaveAllocationValidation", message);
    public static Error UpdateLeaveAllocationValidation(string message) => new Error("LeaveAllocation.UpdateLeaveAllocationValidation", message);
}
