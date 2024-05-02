using MediatR;
using CleanArch.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController(IMediator mediator) : BaseController(mediator)
{
}
