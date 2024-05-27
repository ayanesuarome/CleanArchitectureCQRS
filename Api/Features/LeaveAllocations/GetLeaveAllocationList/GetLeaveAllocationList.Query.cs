using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveAllocations;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    public sealed record Query : IQuery<LeaveAllocationListDto>;
}
