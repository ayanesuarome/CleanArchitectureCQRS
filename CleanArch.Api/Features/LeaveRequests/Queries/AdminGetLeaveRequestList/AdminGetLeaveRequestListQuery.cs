using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.AdminGetLeaveRequestList;

public record AdminGetLeaveRequestListQuery : IRequest<Result<List<LeaveRequestDto>>>
{
}
