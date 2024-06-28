using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.LeaveTypes.Events;

/// <summary>
/// Represents the event that is raised when a leave type is created.
/// </summary>
public sealed record LeaveTypeCreatedDomainEvent(LeaveTypeId LeaveTypeId) : DomainEvent;
