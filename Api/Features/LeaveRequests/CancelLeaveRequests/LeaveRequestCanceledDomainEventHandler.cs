using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public sealed class LeaveRequestCanceledDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<LeaveRequestCanceledDomainEvent>
{
    public async Task Handle(LeaveRequestCanceledDomainEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new LeaveRequestCanceledIntegrationEvent(
                notification.Id,
                notification.OcurredOn,
                notification.LeaveRequestId,
                notification.Range.StartDate,
                notification.Range.EndDate,
                notification.RequestingEmployeeId),
            cancellationToken);
    }
}
