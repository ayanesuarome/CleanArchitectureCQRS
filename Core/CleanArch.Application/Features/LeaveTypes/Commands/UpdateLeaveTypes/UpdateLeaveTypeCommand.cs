using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveTypes;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}
