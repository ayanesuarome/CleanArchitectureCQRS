using CleanArch.Api.Features.LeaveAllocations;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocation;

public record UpdateLeaveAllocationCommand : BaseCommnad, IRequest<Result>
{
    public int Id { get; set; }
}
