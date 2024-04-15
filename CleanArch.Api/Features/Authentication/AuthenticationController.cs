using AutoMapper;
using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.Authentication;

public sealed partial class AuthenticationController(IMediator mediator, IMapper mapper)
    : BaseController(mediator, mapper)
{
}
