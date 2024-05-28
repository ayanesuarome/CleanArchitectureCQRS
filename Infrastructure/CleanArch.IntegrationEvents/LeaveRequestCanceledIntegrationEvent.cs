using CleanArch.Application.EventBus;

namespace CleanArch.IntegrationEvents;

public sealed record LeaveRequestCanceledIntegrationEvent(
    Guid Id,
    DateTimeOffset OcurredOn,
    Guid LeaveRequestId,
    DateOnly StartDate,
    DateOnly EndDate,
    Guid EmployeeId) : IntegrationEvent(Id, OcurredOn);
