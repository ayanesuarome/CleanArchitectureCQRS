using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Authentication;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests;

[HasPermission(Permissions.AccessLeaveRequests)]
public sealed partial class AdminLeaveRequestController(ISender mediator, IPublisher publisher) : BaseAdminController(mediator, publisher)
{
}
