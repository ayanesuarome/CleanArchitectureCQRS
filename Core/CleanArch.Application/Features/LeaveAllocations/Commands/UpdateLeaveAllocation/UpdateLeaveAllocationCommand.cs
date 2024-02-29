using CleanArch.Application.Features.LeaveAllocations.Shared;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public record UpdateLeaveAllocationCommand : BaseLeaveAllocationCommnad, IRequest
{
    public int Id { get; set; }
}
