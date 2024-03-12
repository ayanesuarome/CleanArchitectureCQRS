using CleanArch.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Controllers;

[ApiController]
[Authorize(Roles = Roles.Administrator)]
public abstract class BaseAdminController : ControllerBase
{
}
