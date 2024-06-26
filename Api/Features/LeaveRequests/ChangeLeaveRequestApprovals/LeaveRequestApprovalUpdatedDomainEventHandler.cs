using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;
using CleanArch.Application.Abstractions.Data;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public sealed class LeaveRequestApprovalUpdatedDomainEventHandler : IDomainEventHandler<LeaveRequestApprovalUpdatedDomainEvent>
{
    private readonly ILeaveRequestSummaryRepository _summaryRepository;
    private readonly IReadUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public LeaveRequestApprovalUpdatedDomainEventHandler(
        ILeaveRequestSummaryRepository summaryRepository,
        IReadUnitOfWork unitOfWork,
        IEventBus eventBus)
    {
        _summaryRepository = summaryRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task Handle(LeaveRequestApprovalUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        LeaveRequestSummary summary = await _summaryRepository.GetByIdAsync(notification.LeaveRequestId);

        LeaveRequestSummary summaryWithApprovalUpdated = summary with
        {
            IsApproved = notification.IsApproved
        };

        _summaryRepository.Update(summaryWithApprovalUpdated);
        await _unitOfWork.SaveChangesAsync();

        await _eventBus.PublishAsync(
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
