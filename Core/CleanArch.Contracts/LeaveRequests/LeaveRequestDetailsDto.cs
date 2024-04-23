using CleanArch.Contracts.Identity;
using CleanArch.Contracts.LeaveTypes;

namespace CleanArch.Contracts.LeaveRequests;

public sealed record LeaveRequestDetailsDto(
    DateOnly StartDate,
    DateOnly EndDate,
    string? RequestComments,
    int LeaveTypeId,
    string LeaveTypeName,
    string RequestingEmployeeId,
    string EmployeeFullName,
    bool? IsApproved,
    bool IsCancelled,
    DateTimeOffset DateCreated)
{
}
