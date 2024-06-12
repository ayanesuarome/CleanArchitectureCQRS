using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    public sealed record Query(Guid Id) : IQuery<Response>;
}