using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public class LeaveRequestApprovalUpdatedDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<LeaveRequestApprovalUpdatedDomainEvent>
{
    public async Task Handle(LeaveRequestApprovalUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new LeaveRequestApprovalUpdatedIntegrationEvent(
                notification.Id,
                notification.OcurredOn,
                notification.LeaveRequest.Id,
                notification.LeaveRequest.Range.StartDate,
                notification.LeaveRequest.Range.EndDate,
                notification.LeaveRequest.RequestingEmployeeId,
                notification.LeaveRequest.IsApproved),
            cancellationToken);
    }
}
