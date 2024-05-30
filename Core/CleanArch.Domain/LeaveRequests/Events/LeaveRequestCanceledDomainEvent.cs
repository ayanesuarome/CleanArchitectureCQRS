using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveRequests.Events;

/// <summary>
/// Represents the event that is raised when a leave request is cancel.
/// </summary>
public sealed record LeaveRequestCanceledDomainEvent(
    LeaveRequestId LeaveRequestId,
    DateRange Range,
    Guid RequestingEmployeeId,
    bool IsCancelled) : DomainEvent;
