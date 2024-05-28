using MediatR;

namespace CleanArch.Application.EventBus;

/// <summary>
/// Represents the integration event interface.
/// </summary>
public interface IIntegrationEvent : INotification
{
    /// <summary>
    /// Gets the integration event identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the occurred on date and time.
    /// </summary>
    DateTimeOffset OcurredOn { get; }
}
