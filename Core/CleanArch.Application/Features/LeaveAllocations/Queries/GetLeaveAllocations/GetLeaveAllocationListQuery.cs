using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;

public record GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>
{
}
