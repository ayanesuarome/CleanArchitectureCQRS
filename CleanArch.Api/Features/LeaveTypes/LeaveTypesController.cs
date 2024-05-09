using MediatR;
using CleanArch.Api.Infrastructure;
using CleanArch.Identity.Authentication;
using CleanArch.Domain.Enumerations;

namespace CleanArch.Api.Features.LeaveTypes;

[HasPermission(Permission.AccessLeaveTypes)]
public sealed partial class LeaveTypesController(ISender sender, IPublisher publisher) : BaseController(sender, publisher)
{
}
