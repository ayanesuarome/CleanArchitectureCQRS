using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public record CreateLeaveRequestCommand : BaseLeaveRequestCommand, IRequest<Result<LeaveRequest>>
{
    public int LeaveTypeId { get; set; }
    public string? RequestComments { get; set; }
}
