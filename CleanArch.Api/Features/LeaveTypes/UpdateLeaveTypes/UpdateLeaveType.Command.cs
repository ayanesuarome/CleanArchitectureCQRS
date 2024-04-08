using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    public sealed record Command(int Id, string? Name, int DefaultDays) : IRequest<Result<Unit>>
    {
    }
}
