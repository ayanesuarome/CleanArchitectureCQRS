using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    public sealed record Query(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int Page,
        int PageSize) : IQuery<Response>;
}
