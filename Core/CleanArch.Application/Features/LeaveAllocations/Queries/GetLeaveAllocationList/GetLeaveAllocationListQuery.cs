using CleanArch.Application.Models;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;

public record GetLeaveAllocationListQuery : IRequest<Result<List<LeaveAllocationDto>>>
{
}
