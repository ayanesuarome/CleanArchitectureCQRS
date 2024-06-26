using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;
using CleanArch.Application.Abstractions.Data;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public sealed class LeaveRequestCanceledDomainEventHandler : IDomainEventHandler<LeaveRequestCanceledDomainEvent>
{
    private readonly ILeaveRequestSummaryRepository _summaryRepository;
    private readonly IReadUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public LeaveRequestCanceledDomainEventHandler(
        ILeaveRequestSummaryRepository summaryRepository,
        IReadUnitOfWork unitOfWork,
        IEventBus eventBus)
    {
        _summaryRepository = summaryRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task Handle(LeaveRequestCanceledDomainEvent notification, CancellationToken cancellationToken)
    {
        LeaveRequestSummary summary = await _summaryRepository.GetByIdAsync(notification.LeaveRequestId);
        
        LeaveRequestSummary summaryCanceled = summary with
        {
            IsCancelled = notification.IsCancelled
        };

        _summaryRepository.Update(summaryCanceled);
        await _unitOfWork.SaveChangesAsync();

        await _eventBus.PublishAsync(
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
