namespace CleanArch.Application.EventBus;

/// <summary>
/// Represents the abstract integration event primitive.
/// </summary>
public abstract record IntegrationEvent(Guid Id, DateTimeOffset OcurredOn) : IIntegrationEvent;
