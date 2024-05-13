using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Enumerations;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes;

[HasPermission(Permission.AccessLeaveTypes)]
public sealed partial class AdminLeaveTypeController(ISender sender, IPublisher publisher) : BaseAdminController(sender, publisher)
{
}
