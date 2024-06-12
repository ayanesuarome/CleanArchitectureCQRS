using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    public sealed record Query(Guid Id) : IQuery<Response>;
}
