using CleanArch.Api.Infrastructure;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes;

public sealed partial class AdminLeaveTypeController(ISender sender, IPublisher publisher) : BaseAdminController(sender, publisher)
{
}
