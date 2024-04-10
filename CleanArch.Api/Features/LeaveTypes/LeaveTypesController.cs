using MediatR;
using AutoMapper;
using CleanArch.Api.Infrastructure;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController(IMediator mediator, IMapper mapper)
    : BaseController(mediator, mapper)
{
}
