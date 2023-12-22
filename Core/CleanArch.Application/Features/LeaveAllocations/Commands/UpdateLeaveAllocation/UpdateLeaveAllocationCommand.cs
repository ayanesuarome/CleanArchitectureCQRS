using CleanArch.Application.Features.LeaveAllocations.Shared;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommand : BaseLeaveAllocationCommnad, IRequest
{
    public int Id { get; set; }
}
