using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public record UpdateLeaveRequestCommand : BaseLeaveRequestCommand, IRequest<Result<LeaveRequest>>
{
    public int Id { get; set; }
    public string? RequestComments { get; set; }
    public bool IsCancelled { get; set; }
}
