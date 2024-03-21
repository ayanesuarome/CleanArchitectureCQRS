using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Models;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery : IRequest<Result<List<LeaveRequestDto>>>
{
}
