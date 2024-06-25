using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestDetails;

public static partial class GetLeaveRequestDetail
{
    internal sealed record Query(Guid Id) : IQuery<LeaveRequestSummary>;
}
