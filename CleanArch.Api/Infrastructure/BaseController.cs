using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Infrastructure;

[Route("api/v{version:apiVersion}")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public abstract class BaseController : ControllerBase
{
    protected ISender Sender { get; }
    protected IPublisher Publisher { get; }

    protected BaseController(ISender sender, IPublisher publisher)
    {
        Sender = sender;
        Publisher = publisher;
    }
}
