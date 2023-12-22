using CleanArch.Application.Features.LeaveRequests.Shared;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequestCommand, IRequest<int>
{
    public int LeaveTypeId { get; set; }
    public string? RequestComments { get; set; }
}
