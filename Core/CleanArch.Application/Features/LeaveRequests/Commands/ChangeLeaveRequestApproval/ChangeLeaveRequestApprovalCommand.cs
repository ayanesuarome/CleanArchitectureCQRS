using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public record ChangeLeaveRequestApprovalCommand() : IRequest<Result<LeaveRequest>>
{
    public int Id { get; set; }
    public bool? Approved { get; set; }
}
