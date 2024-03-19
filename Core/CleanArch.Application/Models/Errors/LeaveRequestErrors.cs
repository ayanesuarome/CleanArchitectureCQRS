using CleanArch.Domain.Entities;
using FluentValidation.Results;
using System.Net;

namespace CleanArch.Application.Models.Errors;

public static class LeaveRequestErrors
{
    public static Error NotFound(int id) => new Error("LeaveRequest.NotFound", $"{nameof(LeaveRequest)} with Id '{id}' not found");
    public static Error InvalidApprovalRequest(ValidationResult validationResult) => new Error("LeaveRequest.InvalidApprovalRequest", validationResult.Errors.ToString());
}
