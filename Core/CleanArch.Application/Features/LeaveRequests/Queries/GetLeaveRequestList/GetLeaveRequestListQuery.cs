using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery : IRequest<List<LeaveRequestDto>>
{
}
