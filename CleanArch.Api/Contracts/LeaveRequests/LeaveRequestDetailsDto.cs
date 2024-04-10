using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Application.Models.Identity;

namespace CleanArch.Api.Contracts.LeaveRequests;

public sealed record LeaveRequestDetailsDto(
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    string? RequestComments,
    bool? IsApproved,
    bool IsCancelled,
    int LeaveTypeId,
    LeaveTypeDetailDto? LeaveType,
    string RequestingEmployeeId,
    Employee Employee,
    DateTimeOffset DateRequested,
    DateTimeOffset DateActioned
    ) : BaseDto
{
}
