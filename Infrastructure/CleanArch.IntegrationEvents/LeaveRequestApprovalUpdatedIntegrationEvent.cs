using CleanArch.Application.EventBus;

namespace CleanArch.IntegrationEvents;

public sealed record LeaveRequestApprovalUpdatedIntegrationEvent(
    Guid Id,
    DateTimeOffset OcurredOn,
    Guid LeaveRequestId,
    DateOnly StartDate,
    DateOnly EndDate,
    Guid EmployeeId,
    bool IsApproved) : IntegrationEvent(Id, OcurredOn);
