using CleanArch.Api.Infrastructure;
using CleanArch.Domain.Enumerations;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests;

[HasPermission(Permission.AccessLeaveRequests)]
public sealed partial class LeaveRequestController(ISender mediator, IPublisher publisher) : BaseApiController(mediator, publisher)
{
}
