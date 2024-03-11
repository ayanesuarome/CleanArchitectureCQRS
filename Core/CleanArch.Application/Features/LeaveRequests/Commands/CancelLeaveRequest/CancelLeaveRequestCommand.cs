using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;

public record CancelLeaveRequestCommand(int Id) : IRequest<LeaveRequest>
{
}
