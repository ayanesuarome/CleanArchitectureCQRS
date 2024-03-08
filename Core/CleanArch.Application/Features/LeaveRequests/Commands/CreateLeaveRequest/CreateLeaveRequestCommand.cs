using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public record CreateLeaveRequestCommand : BaseLeaveRequestCommand, IRequest<LeaveRequest>
{
    public int LeaveTypeId { get; set; }
    public string? RequestComments { get; set; }
}
