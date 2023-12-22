using CleanArch.Application.Features.LeaveAllocations.Shared;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : BaseLeaveAllocationCommnad, IRequest<int>
{
    public string? EmployeeId { get; set; }
}
