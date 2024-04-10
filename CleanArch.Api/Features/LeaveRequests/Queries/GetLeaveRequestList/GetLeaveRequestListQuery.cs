using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery : IRequest<Result<List<LeaveRequestDto>>>
{
}
