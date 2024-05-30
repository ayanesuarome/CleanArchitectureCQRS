using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;

namespace CleanArch.Api.Features.LeaveRequests.NotifyLeaveRequestActions;

public sealed class LeaveRequestCreatedDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<LeaveRequestCreatedDomainEvent>
{
    public async Task Handle(LeaveRequestCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        string? comments = null;

        if (notification.Comments is not null)
        {
            comments = notification.Comments;
        }

        await eventBus.PublishAsync(
            new LeaveRequestCreatedIntegrationEvent(
                notification.Id,
                notification.OcurredOn,
                notification.LeaveRequestId,
                notification.Range.StartDate,
                notification.Range.EndDate,
                notification.RequestingEmployeeId,
                comments),
            cancellationToken);
    }
}
