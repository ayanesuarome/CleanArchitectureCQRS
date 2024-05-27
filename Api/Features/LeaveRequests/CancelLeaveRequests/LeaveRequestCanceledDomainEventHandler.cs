using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public class LeaveRequestCanceledDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<LeaveRequestCanceledDomainEvent>
{
    public async Task Handle(LeaveRequestCanceledDomainEvent notification, CancellationToken cancellationToken)
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
