using AutoMapper;
using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class AdminLeaveAllocationController(IMediator mediator, IMapper mapper) :
    BaseAdminController(mediator, mapper)
{
}
