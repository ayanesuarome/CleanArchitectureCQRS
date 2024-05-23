using CleanArch.Api.Infrastructure;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes;

[HasPermission(LeaveTypePermissions.AccessLeaveTypes)]
public sealed partial class AdminLeaveTypeController(ISender sender) : BaseAdminController(sender);
