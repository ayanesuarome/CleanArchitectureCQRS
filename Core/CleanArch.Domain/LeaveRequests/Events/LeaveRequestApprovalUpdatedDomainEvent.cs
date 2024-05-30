using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.LeaveTypes;

namespace CleanArch.Domain.LeaveRequests.Events;

/// <summary>
/// Represents the event that is raised when a leave request is approved or rejected.
/// </summary>
public sealed record LeaveRequestApprovalUpdatedDomainEvent(
    LeaveRequestId LeaveRequestId,
    DateRange Range,
    Guid RequestingEmployeeId,
    bool IsApproved) : DomainEvent;
