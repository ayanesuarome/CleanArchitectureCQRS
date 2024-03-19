using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Models;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.AdminGetLeaveRequestList;

public record AdminGetLeaveRequestListQuery : IRequest<Result<List<LeaveRequestDto>>>
{
}
