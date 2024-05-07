using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using System.Globalization;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed record Command : ICommand<Result<LeaveRequest>>
    {
        public Command(Guid leaveTypeId, string startDate, string endDate, string? comments)
        {
            LeaveTypeId = leaveTypeId;
            StartDate = DateOnly.Parse(startDate, CultureInfo.InvariantCulture);
            EndDate = DateOnly.Parse(endDate, CultureInfo.InvariantCulture); ;
            Comments = comments;
        }

        public Guid LeaveTypeId { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
        public string? Comments { get; init; }
    }
}
