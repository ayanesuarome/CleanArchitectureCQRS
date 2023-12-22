using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommand() : IRequest
{
    public int Id { get; set; }
    public bool? Approved { get; set; }
}
