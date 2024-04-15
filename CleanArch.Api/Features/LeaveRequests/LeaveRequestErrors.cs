using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests;

internal static class LeaveRequestErrors
{
    internal static Error RequestCommentsMaximumLength(string length) => new Error(
        "LeaveRequest.RequestCommentsMaximumLength",
        $"The RequestComments must be up to {length}.");

    internal static Error StartDateLowerThanEndDate => new Error(
        "LeaveRequest.StartDateMustBeLowerThanEndDate",
        "The StartDate must be before EndDate.");

    internal static Error EndDateGeatherThanStartDate => new Error(
        "LeaveRequest.EndDateGeatherThanStartDate",
        "The EndDate must be after StartDate.");

    internal static Error IdIsRequired => new Error("LeaveRequest.IdIsRequired", "The Id is required.");

    internal static Error InvalidLeaveRequest(IDictionary<string, string[]> errors) => new Error(
        "LeaveRequest.InvalidLeaveRequest",
        "Invalid Leave request", errors);

    internal static Error InvalidApprovalRequest(IDictionary<string, string[]> errors) => new Error(
        "LeaveRequest.InvalidApprovalRequest",
        "Invalid approval request", errors);

    internal static Error CreateLeaveRequestValidation(string message) => new Error($"CreateLeaveRequest.Validation", message);

    internal static Error UpdateLeaveRequestValidation(string message) => new Error($"UpdateLeaveRequest.Validation", message);
}
