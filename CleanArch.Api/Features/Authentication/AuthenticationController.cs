using CleanArch.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Features.Authentication;

[AllowAnonymous]
[Route("api/v{version:apiVersion}")]
public sealed partial class AuthenticationController(ISender mediator, IPublisher publisher) : BaseController(mediator, publisher)
{
}
