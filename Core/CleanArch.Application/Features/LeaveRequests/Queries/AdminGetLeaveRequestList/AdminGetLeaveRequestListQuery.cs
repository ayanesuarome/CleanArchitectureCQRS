using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.AdminGetLeaveRequestList;

public record AdminGetLeaveRequestListQuery : IRequest<List<LeaveRequestDto>>
{
}
