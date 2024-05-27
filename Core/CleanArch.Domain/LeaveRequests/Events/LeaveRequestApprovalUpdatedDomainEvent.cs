using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveRequests.Events;

/// <summary>
/// Represents the event that is raised when a leave request is approved or rejected.
/// </summary>
public sealed record LeaveRequestApprovalUpdatedDomainEvent(LeaveRequest LeaveRequest) : DomainEvent;
