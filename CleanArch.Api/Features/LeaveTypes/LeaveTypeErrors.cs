using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveTypes;

internal static class ValidationErrors
{
    internal static class CreateLeaveType
    {
        internal static Error NameIsRequired => new("CreateLeaveType.NameIsRequired", "The Name is required.");
        internal static Error DefaultDaysIsRequired => new("CreateLeaveType.DefaultDaysIsRequired", "The DefaultDays is required.");
    }

    internal static class UpdateLeaveType
    {
        internal static Error IdIsRequired => new("UpdateLeaveType.IdIsRequired", "The Id is required.");
        internal static Error NameIsRequired => new("UpdateLeaveType.NameIsRequired", "The Name is required.");
        internal static Error DefaultDaysIsRequired => new("UpdateLeaveType.DefaultDaysIsRequired", "The DefaultDays is required.");
    }
}
