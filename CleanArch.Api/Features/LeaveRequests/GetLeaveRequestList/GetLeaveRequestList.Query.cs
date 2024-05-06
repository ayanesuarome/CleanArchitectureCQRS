using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    public sealed record Query : IQuery<Result<LeaveRequestListDto>>
    {
    }
}
