using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    public sealed record Query : IRequest<Result<LeaveRequestListDto>>
    {
    }
}
