using CleanArch.Application.ResultPattern;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    public record Command(string Name, int DefaultDays) : IRequest<Result<int>>
    {
    }
}
