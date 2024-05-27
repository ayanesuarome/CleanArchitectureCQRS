namespace IntegrationEvents
{
    public sealed record LeaveRequestCreatedIntegrationEvent(
        Guid Id,
        DateTimeOffset OcurredOn,
        Guid LeaveRequestId,
        DateOnly StartDate,
        DateOnly EndDate,
        string EmployeeId) : IntegrationEvent(Id, OcurredOn);

}
