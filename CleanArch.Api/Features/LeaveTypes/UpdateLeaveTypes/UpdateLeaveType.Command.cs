using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    public sealed record Command(string? Name, int DefaultDays) : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }
}
