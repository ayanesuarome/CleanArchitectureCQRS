using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    public sealed record Query : IRequest<Result<LeaveRequestListDto>>
    {
    }
}
