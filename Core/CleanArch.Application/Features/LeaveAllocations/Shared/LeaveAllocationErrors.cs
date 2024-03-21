using CleanArch.Application.Models.Errors;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Features.LeaveAllocations.Shared;

public static class LeaveAllocationErrors
{
    public static Error NotFound(int id) => new Error("LeaveAllocation.NotFound", $"{nameof(LeaveAllocation)} with Id '{id}' not found");
    //public static Error InvalidApprovalRequest(ValidationResult validationResult) => new Error("LeaveAllocation.InvalidApprovalRequest", validationResult.Errors.ToString());
}
