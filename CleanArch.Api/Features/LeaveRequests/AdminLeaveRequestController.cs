using CleanArch.Api.Infrastructure;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests;

[HasPermission(LeaveRequestPermissions.AccessLeaveRequests)]
public sealed partial class AdminLeaveRequestController(ISender sender) : BaseAdminController(sender);
