using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    public sealed record Query(Guid Id) : IQuery<Result<LeaveAllocationDetailsDto>>
    {
    }
}