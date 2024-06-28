using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveTypes.Events;

/// <summary>
/// Represents the event that is raised when a leave type is updated.
/// </summary>
public sealed record LeaveTypeUpdatedDomainEvent(LeaveTypeId LeaveTypeId) : DomainEvent;
