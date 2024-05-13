using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController(ISender mediator, IPublisher publisher) : BaseAdminController(mediator, publisher)
{
}
