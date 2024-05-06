using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    public sealed record Query : IQuery<Result<LeaveRequestListDto>>
    {
    }
}
