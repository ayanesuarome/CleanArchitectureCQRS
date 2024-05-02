using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController(ISender mediator, IPublisher publisher) : BaseAdminController(mediator, publisher)
{
}
