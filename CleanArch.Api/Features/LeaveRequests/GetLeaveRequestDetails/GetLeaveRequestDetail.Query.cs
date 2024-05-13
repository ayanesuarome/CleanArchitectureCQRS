using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    public sealed record Query(Guid Id) : IQuery<Result<LeaveRequestDetailsDto>>
    {
    }
}
