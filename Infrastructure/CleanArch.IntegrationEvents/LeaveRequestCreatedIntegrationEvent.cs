using CleanArch.Application.EventBus;

namespace CleanArch.IntegrationEvents;

public sealed record LeaveRequestCreatedIntegrationEvent(
    Guid Id,
    DateTimeOffset OcurredOn,
    Guid LeaveRequestId,
    DateOnly StartDate,
    DateOnly EndDate,
    Guid EmployeeId,
    string? Comments) : IntegrationEvent(Id, OcurredOn);
