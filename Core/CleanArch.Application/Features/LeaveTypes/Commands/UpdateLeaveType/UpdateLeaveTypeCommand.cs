using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;

public record UpdateLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int DefaultDays { get; set; }
}