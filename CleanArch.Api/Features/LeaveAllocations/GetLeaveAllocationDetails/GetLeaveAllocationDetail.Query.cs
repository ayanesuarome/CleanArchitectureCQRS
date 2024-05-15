using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveAllocations;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    public sealed record Query(Guid Id) : IQuery<LeaveAllocationDetailsDto>
    {
    }
}