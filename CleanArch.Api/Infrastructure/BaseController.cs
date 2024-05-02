using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Infrastructure;

[Route("api/v{version:apiVersion}")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public abstract class BaseController : ControllerBase
{
    protected readonly ISender _sender;
    protected readonly IPublisher _publisher;

    protected BaseController(ISender sender, IPublisher publisher)
    {
        _sender = sender;
        _publisher = publisher;
    }
}
