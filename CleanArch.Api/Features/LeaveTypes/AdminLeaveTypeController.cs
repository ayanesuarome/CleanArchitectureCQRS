using AutoMapper;
using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class AdminLeaveTypeController(IMediator mediator, IMapper mapper)
    : BaseAdminController(mediator, mapper)
{
}
