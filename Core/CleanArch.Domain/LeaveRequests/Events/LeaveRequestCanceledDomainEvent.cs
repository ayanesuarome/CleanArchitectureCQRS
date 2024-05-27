using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveRequests.Events;

/// <summary>
/// Represents the event that is raised when a leave request is cancel.
/// </summary>
public sealed record LeaveRequestCanceledDomainEvent(LeaveRequest LeaveRequest) : DomainEvent;
