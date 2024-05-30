using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public sealed class LeaveRequestApprovalUpdatedDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<LeaveRequestApprovalUpdatedDomainEvent>
{
    public async Task Handle(LeaveRequestApprovalUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new LeaveRequestApprovalUpdatedIntegrationEvent(
                notification.Id,
                notification.OcurredOn,
                notification.LeaveRequestId,
                notification.Range.StartDate,
                notification.Range.EndDate,
                notification.RequestingEmployeeId,
                notification.IsApproved),
            cancellationToken);
    }
}
