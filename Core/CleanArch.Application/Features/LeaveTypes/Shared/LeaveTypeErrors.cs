using CleanArch.Application.Models.Errors;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Features.LeaveTypes.Shared;

public static class LeaveTypeErrors
{
    public static Error NotFound(int id) => new Error("LeaveType.NotFound", $"{nameof(LeaveType)} with Id '{id}' not found");
    //public static Error InvalidApprovalRequest(ValidationResult validationResult) => new Error("LeaveType.InvalidApprovalRequest", validationResult.Errors.ToString());
}
