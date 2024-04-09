using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Features.LeaveAllocations.Shared;

public static class LeaveAllocationErrors
{
    public static Error NotFound(int id) => new Error($"{nameof(LeaveAllocation)}.NotFound", $"{nameof(LeaveAllocation)} with Id '{id}' not found");
    public static Error InvalidLeaveAllocation(IDictionary<string, string[]> errors) => new Error($"{nameof(LeaveAllocation)}.InvalidLeaveAllocation", $"Invalid {nameof(LeaveAllocation)}", errors);
}
