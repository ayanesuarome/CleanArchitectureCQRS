using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController(IMediator mediator) : BaseController(mediator)
{
}
