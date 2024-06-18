using CleanArch.Api.Contracts;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.Contracts;
using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    public sealed record Query(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int Page,
        int PageSize) : IQuery<Response>;
}
