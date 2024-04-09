using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery : IRequest<Result<List<LeaveRequestDto>>>
{
}
