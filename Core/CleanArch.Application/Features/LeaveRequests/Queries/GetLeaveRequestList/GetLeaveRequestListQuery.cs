using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery : IRequest<List<LeaveRequestDto>>
{
}
