using CleanArch.Api.Features.LeaveRequests;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public record UpdateLeaveRequestCommand : BaseCommand, IRequest<Result<LeaveRequest>>
{
    public int Id { get; set; }
    public string? RequestComments { get; set; }
    public bool IsCancelled { get; set; }
}
