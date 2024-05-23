using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveRequests.Events;

/// <summary>
/// Represents the event that is raised when a leave request is created.
/// </summary>
public sealed record LeaveRequestCreatedDomainEvent(LeaveRequest LeaveRequest) : DomainEvent;
