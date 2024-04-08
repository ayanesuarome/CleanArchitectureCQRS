using AutoMapper;
using CleanArch.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Api.Infrastructure;

[Authorize(Roles = Roles.Administrator)]
public abstract class BaseAdminController : BaseApiController
{
    protected BaseAdminController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }
}
