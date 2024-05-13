using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    public sealed record Query : IQuery<Result<LeaveAllocationListDto>>
    {
    }
}
