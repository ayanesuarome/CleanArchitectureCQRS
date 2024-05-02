using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController(ISender mediator, IPublisher publisher) : BaseController(mediator, publisher)
{
}
