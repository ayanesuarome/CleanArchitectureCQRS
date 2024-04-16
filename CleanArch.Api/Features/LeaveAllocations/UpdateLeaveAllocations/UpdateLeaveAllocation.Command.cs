using CleanArch.Domain.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocations;

public static partial class UpdateLeaveAllocation
{
    public sealed record Command(int LeaveTypeId, int NumberOfDays, int Period) : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
