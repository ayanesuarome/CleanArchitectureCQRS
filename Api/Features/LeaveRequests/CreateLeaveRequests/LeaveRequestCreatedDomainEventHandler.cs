using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;
using CleanArch.Domain.LeaveRequests;
using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Contracts;
using CleanArch.Application.Abstractions.Data;

namespace CleanArch.Api.Features.LeaveRequests.NotifyLeaveRequestActions;

public sealed class LeaveRequestCreatedDomainEventHandler : IDomainEventHandler<LeaveRequestCreatedDomainEvent>
{
    private readonly IUserService _userService;
    private readonly ILeaveRequestSummaryRepository _summaryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public LeaveRequestCreatedDomainEventHandler(
        IUserService userService,
        ILeaveRequestSummaryRepository summaryRepository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus)
    {
        _summaryRepository = summaryRepository;
        _userService = userService;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task Handle(LeaveRequestCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Employee employee = await _userService.GetEmployee(notification.RequestingEmployeeId);

        LeaveRequestSummary summary = new(
            notification.LeaveRequestId.Id,
            notification.Range.StartDate.ToString(),
            notification.Range.EndDate.ToString(),
            notification.Comments,
            notification.LeaveTypeId,
            notification.LeaveTypeName,
            employee.Id,
            employee.FullName,
            notification.IsApproved,
            notification.IsCancelled,
            notification.DateCreated);

        _summaryRepository.Add(summary);
        await _unitOfWork.SaveChangesAsync();

        //string? comments = null;

        //if (notification?.Comments is not null)
        //{
        //    comments = notification.Comments;
        //}

        await _eventBus.PublishAsync(
            new LeaveRequestCreatedIntegrationEvent(
                notification.Id,
                notification.OcurredOn,
                notification.LeaveRequestId,
                notification.Range.StartDate,
                notification.Range.EndDate,
                notification.RequestingEmployeeId,
                notification.Comments?.Value),
            cancellationToken);
    }
}
