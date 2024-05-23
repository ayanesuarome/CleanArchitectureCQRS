using CleanArch.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Api.Features.Authentication;

[AllowAnonymous]
public sealed partial class AuthenticationController(ISender sender) : BaseController(sender);
