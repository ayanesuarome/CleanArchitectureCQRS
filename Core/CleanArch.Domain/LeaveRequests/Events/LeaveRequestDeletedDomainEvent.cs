using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveRequests.Events;

/// <summary>
/// Represents the event that is raised when a leave request is removed.
/// </summary>
public sealed record LeaveRequestDeletedDomainEvent(LeaveRequestId LeaveRequestId) : DomainEvent;
