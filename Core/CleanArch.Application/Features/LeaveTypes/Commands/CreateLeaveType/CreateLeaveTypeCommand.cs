using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;

public record CreateLeaveTypeCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}
