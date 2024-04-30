using MediatR;
using CleanArch.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Api.Features.LeaveTypes;

[Route("api/v{version:apiVersion}")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public sealed partial class LeaveTypesController(IMediator mediator): BaseController(mediator)
{
}
