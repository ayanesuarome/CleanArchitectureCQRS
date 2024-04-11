using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveAllocations;

public static class LeaveAllocationErrors
{
    public static Error IdRequired() => new Error("LeaveAllocation.IdRequired", $"{nameof(LeaveAllocation.Id)} is required.");
    public static Error NumberOfDaysGreatherThan(string message) => new Error("LeaveAllocation.NumberOfDaysGreatherThan", message);
    public static Error PeriodGreaterThanOrEqualToOngoingYear(string message) => new Error("LeaveAllocation.PeriodGreaterThanOrEqualToOngoingYear", message);
    public static Error LeaveTypeMustExist() => new Error("LeaveAllocation.LeaveTypeMustExist", "There must be an associated Leave type.");
    public static Error NotFound(int id) => new Error("LeaveAllocation.NotFound", $"Leave allocation with Id '{id}' not found");
    public static Error InvalidLeaveAllocation(IDictionary<string, string[]> errors) => new Error("LeaveAllocation.InvalidLeaveAllocation", $"Invalid Leave allocation", errors);
    public static Error CreateLeaveAllocationValidation(string message) => new Error("LeaveAllocation.CreateLeaveAllocationValidation", message);
    public static Error UpdateLeaveAllocationValidation(string message) => new Error("LeaveAllocation.UpdateLeaveAllocationValidation", message);
}
