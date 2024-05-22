using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;
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

        public Guid LeaveTypeId { get; }
        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }
        public string? Comments { get; }
    }
}
