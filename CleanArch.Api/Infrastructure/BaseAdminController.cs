using AutoMapper;
using CleanArch.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Infrastructure;

[Route("api/admin/v{version:apiVersion}/[controller]")]
[Authorize(Roles = Roles.Administrator)]
public abstract class BaseAdminController : BaseController
{
    protected BaseAdminController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }
}
