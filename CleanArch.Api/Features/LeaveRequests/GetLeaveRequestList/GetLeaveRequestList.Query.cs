using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    public sealed record Query : IQuery<LeaveRequestListDto>;
}
