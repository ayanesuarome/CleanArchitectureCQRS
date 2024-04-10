using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    public sealed record Query : IRequest<Result<LeaveRequestListDto>>
    {
    }
}
