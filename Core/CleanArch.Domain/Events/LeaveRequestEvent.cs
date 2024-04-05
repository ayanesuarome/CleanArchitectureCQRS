using CleanArch.Domain.Events;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Events;

public class LeaveRequestEvent : IDomainEvent
{
    public LeaveRequest LeaveRequest { get; set; }
    public DateTimeOffset ActionDate { get; private set; }
    public LeaveRequestAction Action { get; private set; }

    public LeaveRequestEvent(LeaveRequest leaveRequest, LeaveRequestAction action, DateTimeOffset dateUpadted)
    {
        LeaveRequest = leaveRequest;
        Action = action;
        ActionDate = dateUpadted;
    }

    public LeaveRequestEvent(LeaveRequest leaveRequest, LeaveRequestAction action)
        : this(leaveRequest, action, DateTimeOffset.Now)
    {
    }
}
