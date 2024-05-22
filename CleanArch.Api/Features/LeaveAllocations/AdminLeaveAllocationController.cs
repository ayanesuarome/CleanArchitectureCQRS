using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Authentication;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations;

[HasPermission(Permissions.AccessLeaveAllocations)]
public sealed partial class AdminLeaveAllocationController(ISender mediator, IPublisher publisher)
    : BaseAdminController(mediator, publisher)
{
}
