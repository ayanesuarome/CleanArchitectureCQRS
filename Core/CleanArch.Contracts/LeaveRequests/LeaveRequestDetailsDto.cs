using CleanArch.Contracts.Identity;
using CleanArch.Contracts.LeaveTypes;

namespace CleanArch.Contracts.LeaveRequests;

public sealed record LeaveRequestDetailsDto(
    DateOnly StartDate,
    DateOnly EndDate,
    string? RequestComments,
    Guid LeaveTypeId,
    string LeaveTypeName,
    Guid RequestingEmployeeId,
    string EmployeeFullName,
    bool? IsApproved,
    bool IsCancelled,
    DateTimeOffset DateCreated)
{
}
