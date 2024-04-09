using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveTypes;

public static class LeaveTypeErrors
{
    public static Error IdRequired() => new Error("LeaveType.IdIsRequired", $"The {nameof(LeaveType.Id)} is required.");
    public static Error NameRequired() => new Error("LeaveType.NameIsRequired", $"The {nameof(LeaveType.Name)} is required.");
    public static Error NameMaximumLength(int maxLength) => new Error("LeaveType.NameMaximumLengthIsOutOfRange", $"The {nameof(LeaveType.Name)} must be up to {maxLength} characters.");
    public static Error NameUnique() => new Error("LeaveType.NameIsUnique", "Leave type already exist.");
    public static Error DefaultDaysRange(string range) => new Error("LeaveType.DefaultDaysNotInRange", $"The {nameof(LeaveType.DefaultDays)} must be between {range}.");
    public static Error NotFound(int id) => new Error($"LeaveType.NotFound", $"Leave type with Id '{id}' not found.");

    public static Error InvalidLeaveType(IDictionary<string, string[]> errors)
    {
        return new Error($"LeaveType.InvalidLeaveType", $"Invalid LeaveType", errors);
    }
    
    public static Error CreateLeaveTypeValidation(string message)
    {
        return new Error($"CreateLeaveType.Validation", message);
    }
    
    public static Error UpdateLeaveTypeValidation(string message)
    {
        return new Error($"UpdateLeaveType.Validation", message);
    }
}
