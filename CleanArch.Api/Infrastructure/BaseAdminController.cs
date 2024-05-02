using CleanArch.Contracts.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Infrastructure;

[Route("api/admin/v{version:apiVersion}")]
[Authorize(Roles = Roles.Administrator)]
[ApiController]
public abstract class BaseAdminController : BaseController
{
    protected BaseAdminController(IMediator mediator)
        : base(mediator)
    {
    }
}
