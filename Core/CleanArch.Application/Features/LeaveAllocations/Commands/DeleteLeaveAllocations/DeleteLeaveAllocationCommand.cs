using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocations;

public record DeleteLeaveAllocationCommand(int Id) : IRequest
{
}
