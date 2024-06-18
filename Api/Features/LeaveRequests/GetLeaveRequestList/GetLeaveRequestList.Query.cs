using CleanArch.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    public sealed record Query(string? SearchTerm, string? SortColumn, string? SortOrder) : IQuery<Response>;
}
