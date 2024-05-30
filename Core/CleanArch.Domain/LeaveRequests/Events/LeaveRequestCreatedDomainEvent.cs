using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;

namespace CleanArch.Domain.LeaveRequests.Events;

/// <summary>
/// Represents the event that is raised when a leave request is created.
/// </summary>
public sealed record LeaveRequestCreatedDomainEvent(
    LeaveRequestId LeaveRequestId,
    DateRange Range,
    Name LeaveTypeName,
    LeaveTypeId LeaveTypeId,
    Guid RequestingEmployeeId,
    Comment? Comments) : DomainEvent;
