using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Authentication;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes;

[HasPermission(Permissions.AccessLeaveTypes)]
public sealed partial class AdminLeaveTypeController(ISender sender, IPublisher publisher) : BaseAdminController(sender, publisher)
{
}
