using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;

namespace CleanArch.Api.Features.LeaveRequests.NotifyLeaveRequestActions;

public class LeaveRequestCreatedDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<LeaveRequestCreatedDomainEvent>
{
    public async Task Handle(LeaveRequestCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new LeaveRequestCreatedIntegrationEvent(
                notification.Id,
                notification.OcurredOn,
                notification.LeaveRequest.Id,
                notification.LeaveRequest.Range.StartDate,
                notification.LeaveRequest.Range.EndDate,
                notification.LeaveRequest.RequestingEmployeeId),
            cancellationToken);
    }
}
