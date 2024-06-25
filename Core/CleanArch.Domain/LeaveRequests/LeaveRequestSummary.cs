namespace CleanArch.Domain.LeaveRequests;

public record LeaveRequestSummary(
    Guid Id,
    string StartDate,
    string EndDate,
    string? Comments,
    Guid LeaveTypeId,
    string LeaveTypeName,
    Guid RequestingEmployeeId,
    string EmployeeFullName,
    bool? IsApproved,
    bool IsCancelled,
    DateTimeOffset DateCreated);
