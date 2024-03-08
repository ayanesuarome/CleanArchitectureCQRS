using CleanArch.Domain.Entities;
using CleanArch.Domain.Events;

namespace CleanArch.Application.Events;

public class LeaveRequestCreated : IDomainEvent
{
    public LeaveRequest LeaveRequest { get; set; }
    public DateTimeOffset ActionDate { get; private set; }

    public LeaveRequestCreated(LeaveRequest leaveRequest, DateTimeOffset dateCreated)
    {
        LeaveRequest = leaveRequest;
        ActionDate = dateCreated;
    }

    public LeaveRequestCreated(LeaveRequest leaveRequest) : this(leaveRequest, DateTimeOffset.Now)
    {
        
    }
}
