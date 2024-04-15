using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveTypes;

internal static class LeaveTypeErrors
{
    internal static Error IdIsRequired => new Error("LeaveType.IdIsRequired", "The Id is required.");
    internal static Error NameIsRequired => new Error("LeaveType.NameIsRequired", "The Name is required.");
    internal static Error NameMaximumLength(string maxLength) => new Error("LeaveType.NameMaximumLengthIsOutOfRange", $"The Name must be up to {maxLength} characters.");

    internal static Error DefaultDaysRange(string range) => new Error("LeaveType.DefaultDaysNotInRange", $"The DefaultDays must be between {range}.");

    internal static Error InvalidLeaveType(IDictionary<string, string[]> errors) => new Error("LeaveType.InvalidLeaveType", "Invalid LeaveType", errors);
    internal static Error CreateLeaveTypeValidation(string message) => new Error("CreateLeaveType.Validation", message);
    internal static Error UpdateLeaveTypeValidation(string message) => new Error("UpdateLeaveType.Validation", message);
}
