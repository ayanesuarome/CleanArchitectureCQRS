using CleanArch.Application.Features.LeaveRequests.Shared;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public record UpdateLeaveRequestCommand : BaseLeaveRequestCommand, IRequest
{
    public int Id { get; set; }
    public string? RequestComments { get; set; }
    public bool IsCancelled { get; set; }
}
