using AutoMapper;
using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed partial class LeaveRequestController(IMediator mediator, IMapper mapper)
    : BaseApiController(mediator, mapper)
{
}
