using AutoMapper;
using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class AdminLeaveRequestController(IMediator mediator, IMapper mapper)
    : BaseAdminController(mediator, mapper)
{
}
