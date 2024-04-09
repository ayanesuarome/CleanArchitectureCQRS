using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;

public record DeleteLeaveRequestCommand(int Id) : IRequest<Result>
{
}
