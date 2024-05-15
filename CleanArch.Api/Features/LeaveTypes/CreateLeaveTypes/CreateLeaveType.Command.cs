using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    public sealed record Command(string Name, int DefaultDays) : ICommand<Guid>
    {
    }
}
