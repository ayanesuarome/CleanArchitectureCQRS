using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    public sealed record Query(int Id) : IRequest<Result<LeaveRequestDetailsDto>>
    {
    }
}
