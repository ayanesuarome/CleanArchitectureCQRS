using MediatR;
using CleanArch.Api.Infrastructure;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class LeaveTypesController(ISender sender, IPublisher publisher) : BaseController(sender, publisher)
{
}
