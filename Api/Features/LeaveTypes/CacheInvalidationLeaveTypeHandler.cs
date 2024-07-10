using CleanArch.Application.Abstractions.Caching;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.LeaveTypes.Events;

namespace CleanArch.Api.Features.LeaveTypes;

internal sealed class CacheInvalidationLeaveTypeHandler(ICacheService cacheService) :
    IDomainEventHandler<LeaveTypeCreatedDomainEvent>,
    IDomainEventHandler<LeaveTypeUpdatedDomainEvent>,
    IDomainEventHandler<LeaveTypeDeletedDomainEvent>
{
    public Task Handle(LeaveTypeCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.LeaveTypeId);
    }

    public Task Handle(LeaveTypeUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.LeaveTypeId);
    }

    public Task Handle(LeaveTypeDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.LeaveTypeId);
    }

    private Task HandleInternal(Guid leaveTypeId)
    {
        cacheService.Remove($"leave-type-by-id-{leaveTypeId}");
        cacheService.Remove($"leavetypes");

        return Task.CompletedTask;
    }
}
