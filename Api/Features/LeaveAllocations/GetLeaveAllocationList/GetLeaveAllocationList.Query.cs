using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    public sealed record Query : IQuery<Response>;
}
