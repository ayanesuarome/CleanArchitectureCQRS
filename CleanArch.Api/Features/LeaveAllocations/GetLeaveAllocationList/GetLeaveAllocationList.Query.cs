using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationList;

public static partial class GetLeaveAllocationList
{
    public sealed record Query : IRequest<Result<LeaveAllocationListDto>>
    {
    }
}
