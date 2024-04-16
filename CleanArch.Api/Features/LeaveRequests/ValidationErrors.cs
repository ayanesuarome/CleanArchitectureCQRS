﻿using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests;

internal static class ValidationErrors
{
    internal static class ChangeLeaveRequestApproval
    {
        internal static Error IdIsRequired => new Error("ChangeLeaveRequestApproval.IdIsRequired", "The Id is required.");
        internal static Error ChangeLeaveRequestApprovalValidation(string message) => new Error("ChangeLeaveRequestApproval.Validation", message);
    }

    internal static class CreateLeaveRequest
    {
        internal static Error LeaveTypeIdIsRequired => new Error("CreateLeaveRequest.LeaveTypeIdIsRequired", "The leave type ID is required");
        internal static Error RequestCommentsMaximumLength(string length) => new Error(
        "CreateLeaveRequest.RequestCommentsMaximumLength",
        $"The RequestComments must be up to {length}.");

        internal static Error StartDateLowerThanEndDate => new Error(
        "CreateLeaveRequest.StartDateMustBeLowerThanEndDate",
        "The StartDate must be before EndDate.");

        internal static Error EndDateGeatherThanStartDate => new Error(
            "CreateLeaveRequest.EndDateGeatherThanStartDate",
            "The EndDate must be after StartDate.");

        internal static Error CreateLeaveRequestValidation(string message) => new Error("CreateLeaveRequest.Validation", message);
    }

    internal static class UpdateLeaveRequest
    {
        internal static Error IdIsRequired => new Error("UpdateLeaveRequest.IdIsRequired", "The Id is required.");
        internal static Error StartDateLowerThanEndDate => new Error(
        "UpdateLeaveRequest.StartDateMustBeLowerThanEndDate",
        "The StartDate must be before EndDate.");

        internal static Error EndDateGeatherThanStartDate => new Error(
            "UpdateLeaveRequest.EndDateGeatherThanStartDate",
            "The EndDate must be after StartDate.");

        internal static Error UpdateLeaveRequestValidation(string message) => new Error("UpdateLeaveRequest.Validation", message);

    }
      
    internal static Error InvalidLeaveRequest(IDictionary<string, string[]> errors) => new Error(
        "LeaveRequest.InvalidLeaveRequest",
        "Invalid Leave request", errors);

    internal static Error InvalidApprovalRequest(IDictionary<string, string[]> errors) => new Error(
        "LeaveRequest.InvalidApprovalRequest",
        "Invalid approval request", errors);
}
