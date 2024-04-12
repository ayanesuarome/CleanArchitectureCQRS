using CleanArch.Api.Contracts.LeaveAllocations;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.GetLeaveAllocationDetails;

public static partial class GetLeaveAllocationDetail
{
    public sealed record Query(int Id) : IRequest<Result<LeaveAllocationDetailsDto>>
    {
    }
}
