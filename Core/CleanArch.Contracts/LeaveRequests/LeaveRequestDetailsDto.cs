namespace CleanArch.Contracts.LeaveRequests;

public sealed record LeaveRequestDetailsDto(
    Guid Id,
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
