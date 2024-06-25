using CleanArch.Application.Abstractions.Data;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Domain.LeaveRequests.Events;

namespace CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;

public class LeaveRequestDeletedDomainEventHandler : IDomainEventHandler<LeaveRequestDeletedDomainEvent>
{
    private readonly ILeaveRequestSummaryRepository _summaryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LeaveRequestDeletedDomainEventHandler(
        ILeaveRequestSummaryRepository summaryRepository,
        IUnitOfWork unitOfWork)
    {
        _summaryRepository = summaryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(LeaveRequestDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        LeaveRequestSummary summary = await _summaryRepository.GetByIdAsync(notification.LeaveRequestId);

        _summaryRepository.Delete(summary);
        _unitOfWork.SaveChangesAsync();
    }
}
