using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController(IMediator mediator) : BaseAdminController(mediator)
{
}
