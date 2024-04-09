using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;

public record GetLeaveAllocationListQuery : IRequest<Result<List<LeaveAllocationDto>>>
{
}
