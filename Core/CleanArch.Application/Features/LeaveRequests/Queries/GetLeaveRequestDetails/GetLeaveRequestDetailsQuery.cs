using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery(int Id) : IRequest<LeaveRequestDetailsDto>
{
}
