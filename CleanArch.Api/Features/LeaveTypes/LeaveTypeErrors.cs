using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveTypes;

internal static class ValidationErrors
{
    internal static class CreateLeaveType
    {
        internal static Error NameIsRequired => new("CreateLeaveType.NameIsRequired", "The Name is required.");
        internal static Error DefaultDaysIsRequired => new("CreateLeaveType.DefaultDaysIsRequired", "The DefaultDays is required.");

        internal static Error NameMaximumLength(string maxLength) => new(
            "CreateLeaveType.NameMaximumLengthIsOutOfRange",
            $"The Name must be up to {maxLength} characters.");

        internal static Error CreateLeaveTypeValidation(string message) => new("CreateLeaveType.Validation", message);
    }

    internal static class UpdateLeaveType
    {
        internal static Error IdIsRequired => new("UpdateLeaveType.IdIsRequired", "The Id is required.");
        internal static Error NameIsRequired => new("UpdateLeaveType.NameIsRequired", "The Name is required.");
        internal static Error DefaultDaysIsRequired => new("UpdateLeaveType.DefaultDaysIsRequired", "The DefaultDays is required.");

        internal static Error UpdateLeaveTypeValidation(string message) => new("UpdateLeaveType.Validation", message);
    }

    internal static Error InvalidLeaveType(IDictionary<string, string[]> errors) => new("LeaveType.InvalidLeaveType", "Invalid LeaveType", errors);
    
    
}
