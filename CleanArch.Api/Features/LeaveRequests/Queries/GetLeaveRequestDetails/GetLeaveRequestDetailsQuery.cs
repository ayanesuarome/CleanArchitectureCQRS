using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery(int Id) : IRequest<Result<LeaveRequestDetailsDto>>
{
}
