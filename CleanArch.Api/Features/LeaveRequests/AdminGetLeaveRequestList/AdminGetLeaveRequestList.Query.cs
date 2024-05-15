using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    public sealed record Query : IQuery<LeaveRequestListDto>
    {
    }
}
