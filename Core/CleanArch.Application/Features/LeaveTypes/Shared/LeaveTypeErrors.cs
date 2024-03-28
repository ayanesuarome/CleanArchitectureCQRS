using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Features.LeaveTypes.Shared;

public static class LeaveTypeErrors
{
    public static Error NotFound(int id) => new Error($"{nameof(LeaveType)}.NotFound", $"{nameof(LeaveType)} with Id '{id}' not found");

    public static Error InvalidLeaveType(IDictionary<string, string[]> errors)
    {
        return new Error($"{nameof(LeaveRequest)}.InvalidLeaveType", $"Invalid {nameof(LeaveRequest)}", errors);
    }
}
