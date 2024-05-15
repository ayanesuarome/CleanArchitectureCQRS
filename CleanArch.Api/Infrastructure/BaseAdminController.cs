using CleanArch.Contracts.Identity;
using CleanArch.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Infrastructure;

[Route("api/admin/v{version:apiVersion}")]
public abstract class BaseAdminController : BaseApiController
{
    protected BaseAdminController(ISender sender, IPublisher publisher)
        : base(sender, publisher)
    {
    }
}
