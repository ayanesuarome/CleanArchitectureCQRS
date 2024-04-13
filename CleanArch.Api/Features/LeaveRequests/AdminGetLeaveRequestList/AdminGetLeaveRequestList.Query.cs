using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    public sealed record Query : IRequest<Result<LeaveRequestListDto>>
    {
    }
}
