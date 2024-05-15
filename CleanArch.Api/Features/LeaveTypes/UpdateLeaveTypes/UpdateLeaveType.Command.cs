using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    public sealed record Command(Guid Id, string Name, int DefaultDays) : ICommand<Unit>
    {
    }
}
