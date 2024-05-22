using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests;

internal static class ValidationErrors
{
    internal static class ChangeLeaveRequestApproval
    {
        internal static Error IdIsRequired => new("ChangeLeaveRequestApproval.IdIsRequired", "The Id is required.");
        internal static Error ChangeLeaveRequestApprovalValidation(string message) => new("ChangeLeaveRequestApproval.Validation", message);
    }

    internal static class CreateLeaveRequest
    {
        internal static Error LeaveTypeIdIsRequired => new("CreateLeaveRequest.LeaveTypeIdIsRequired", "The leave type ID is required.");
        internal static Error StartDateIsRequired => new("CreateLeaveRequest.StartDateIsRequired", "The StartDate is required.");
        internal static Error EndDateIsRequired => new("CreateLeaveRequest.EndDateIsRequired", "The EndDate is required.");
        internal static Error RequestCommentsMaximumLength(string length) => new(
        "CreateLeaveRequest.RequestCommentsMaximumLength",
        $"The RequestComments must be up to {length}.");

        internal static Error StartDateLowerThanEndDate => new(
        "CreateLeaveRequest.StartDateMustBeLowerThanEndDate",
        "The StartDate must be before EndDate.");

        internal static Error EndDateGeatherThanStartDate => new(
            "CreateLeaveRequest.EndDateGeatherThanStartDate",
            "The EndDate must be after StartDate.");

        internal static Error CreateLeaveRequestValidation(string message) => new("CreateLeaveRequest.Validation", message);
    }

    internal static class UpdateLeaveRequest
    {
        internal static Error IdIsRequired => new("UpdateLeaveRequest.IdIsRequired", "The Id is required.");
        internal static Error StartDateIsRequired => new("UpdateLeaveRequest.StartDateIsRequired", "The StartDate is required.");
        internal static Error EndDateIsRequired => new("UpdateLeaveRequest.EndDateIsRequired", "The EndDate is required.");
        internal static Error UpdateLeaveRequestValidation(string message) => new("UpdateLeaveRequest.Validation", message);

    }
}
