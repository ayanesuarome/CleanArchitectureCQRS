using CleanArch.Domain.Entities;
using CleanArch.Application.Abstractions.Messaging;
using System.Globalization;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    public sealed record Command : ICommand<LeaveRequest>
    {
        public Command(Guid id, string startDate, string endDate, string? comments)
        {
            Id = id;
            StartDate = DateOnly.Parse(startDate, CultureInfo.InvariantCulture);
            EndDate = DateOnly.Parse(endDate, CultureInfo.InvariantCulture); ;
            Comments = comments;
        }

        public Guid Id { get; }
        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }
        public string? Comments { get; }
    }
}
