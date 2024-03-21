using CleanArch.Application.Models;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;

public record DeleteLeaveAllocationCommand(int Id) : IRequest<Result>
{
}
