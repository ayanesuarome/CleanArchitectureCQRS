using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    public sealed record Query(int Id) : IRequest<Result<LeaveRequestDetailsDto>>
    {
    }
}
