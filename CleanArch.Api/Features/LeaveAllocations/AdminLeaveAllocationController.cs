using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Enumerations;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations;

[HasPermission(Permission.AccessLeaveAllocations)]
public sealed partial class AdminLeaveAllocationController(ISender mediator, IPublisher publisher)
    : BaseAdminController(mediator, publisher)
{
}
