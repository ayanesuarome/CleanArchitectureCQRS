using CleanArch.Contracts.Identity;
using CleanArch.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Infrastructure;

[Route("api/admin/v{version:apiVersion}")]
[Authorize(Roles = "Administrator")]
[ApiController]
public abstract class BaseAdminController : BaseController
{
    protected BaseAdminController(ISender sender, IPublisher publisher)
        : base(sender, publisher)
    {
    }
}
