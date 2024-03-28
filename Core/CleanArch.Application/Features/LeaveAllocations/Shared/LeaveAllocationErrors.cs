using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Features.LeaveAllocations.Shared;

public static class LeaveAllocationErrors
{
    public static Error NotFound(int id) => new Error("LeaveAllocation.NotFound", $"{nameof(LeaveAllocation)} with Id '{id}' not found");
    public static Error InvalidLeaveAllocation(IDictionary<string, string[]> errors) => new Error("LeaveAllocation.InvalidLeaveAllocation", $"Invalid {nameof(LeaveAllocation)}", errors);
    //public static Error InvalidApprovalRequest(ValidationResult validationResult) => new Error("LeaveAllocation.InvalidApprovalRequest", validationResult.Errors.ToString());
}
