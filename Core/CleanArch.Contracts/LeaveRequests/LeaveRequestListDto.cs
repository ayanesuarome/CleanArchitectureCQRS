namespace CleanArch.Contracts.LeaveRequests;

public sealed record LeaveRequestListDto
{
    public LeaveRequestListDto(IReadOnlyCollection<LeaveRequestDetailsModel> leaveRequests) => LeaveRequests = leaveRequests;

    public IReadOnlyCollection<LeaveRequestDetailsModel> LeaveRequests { get; }

    public sealed record LeaveRequestDetailsModel(
        Guid Id,
        string StartDate,
        string EndDate,
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
}
