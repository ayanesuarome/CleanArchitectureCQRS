using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveTypes.Events;

/// <summary>
/// Represents the event that is raised when a leave type is deleted.
/// </summary>
public sealed record LeaveTypeDeletedDomainEvent(LeaveTypeId LeaveTypeId) : DomainEvent;
