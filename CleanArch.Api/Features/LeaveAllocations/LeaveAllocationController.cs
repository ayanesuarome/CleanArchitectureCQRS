using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Enumerations;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations;

[HasPermission(Permission.AccessLeaveAllocations)]
public sealed partial class LeaveAllocationController(ISender mediator, IPublisher publisher) : BaseApiController(mediator, publisher)
{
}
