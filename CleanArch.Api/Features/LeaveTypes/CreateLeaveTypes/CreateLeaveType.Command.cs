using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    public sealed record Command(string Name, int DefaultDays) : IRequest<Result<int>>
    {
    }
}
