using CleanArch.Api.Infrastructure;
using CleanArch.Identity.Authentication;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations;

[HasPermission(LeaveAllocationPermissions.AccessLeaveAllocations)]
public sealed partial class AdminLeaveAllocationController(ISender sender) : BaseAdminController(sender);
