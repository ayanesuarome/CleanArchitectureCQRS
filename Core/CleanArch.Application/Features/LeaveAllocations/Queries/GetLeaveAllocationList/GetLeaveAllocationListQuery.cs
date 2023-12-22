using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;

public record GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>
{
}
