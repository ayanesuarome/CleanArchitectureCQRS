using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;

public static partial class CreateLeaveType
{
    public record Command(string Name, int DefaultDays) : IRequest<int>
    {
    }
}
