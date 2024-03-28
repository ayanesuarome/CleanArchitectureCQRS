using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;

public record UpdateLeaveTypeCommand : IRequest<Result<Unit>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int DefaultDays { get; set; }
}