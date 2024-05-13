using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveTypes;

internal static class ValidationErrors
{
    internal static class CreateLeaveType
    {
        internal static Error NameIsRequired => new Error("CreateLeaveType.NameIsRequired", "The Name is required.");
        internal static Error DefaultDaysIsRequired => new Error("CreateLeaveType.DefaultDaysIsRequired", "The DefaultDays is required.");

        internal static Error NameMaximumLength(string maxLength) => new Error(
            "CreateLeaveType.NameMaximumLengthIsOutOfRange",
            $"The Name must be up to {maxLength} characters.");

        internal static Error CreateLeaveTypeValidation(string message) => new Error("CreateLeaveType.Validation", message);
    }

    internal static class UpdateLeaveType
    {
        internal static Error IdIsRequired => new Error("UpdateLeaveType.IdIsRequired", "The Id is required.");
        internal static Error NameIsRequired => new Error("UpdateLeaveType.NameIsRequired", "The Name is required.");
        internal static Error DefaultDaysIsRequired => new Error("UpdateLeaveType.DefaultDaysIsRequired", "The DefaultDays is required.");

        internal static Error UpdateLeaveTypeValidation(string message) => new Error("UpdateLeaveType.Validation", message);
    }

    internal static Error InvalidLeaveType(IDictionary<string, string[]> errors) => new Error("LeaveType.InvalidLeaveType", "Invalid LeaveType", errors);
    
    
}
