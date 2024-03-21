using CleanArch.Application.Models.Errors;
using CleanArch.Domain.Entities;
using FluentValidation.Results;

namespace CleanArch.Application.Features.LeaveRequests.Shared;

public static class LeaveRequestErrors
{
    public static Error NotFound(int id) => new Error("LeaveRequest.NotFound", $"{nameof(LeaveRequest)} with Id '{id}' not found");
    public static Error InvalidApprovalRequest(ValidationResult validationResult) => new Error("LeaveRequest.InvalidApprovalRequest", validationResult.Errors.ToString());
}
