using AutoMapper;
using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed partial class LeaveAllocationController(IMediator mediator, IMapper mapper)
    : BaseController(mediator, mapper)
{
}
