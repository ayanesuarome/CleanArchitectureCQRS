namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    public sealed record Response(
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
    DateTimeOffset DateCreated);
}
