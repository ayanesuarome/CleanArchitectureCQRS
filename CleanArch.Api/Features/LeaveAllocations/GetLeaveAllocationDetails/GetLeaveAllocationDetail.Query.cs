using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    public sealed record Query(Guid Id) : IRequest<Result<LeaveAllocationDetailsDto>>
    {
    }
}
