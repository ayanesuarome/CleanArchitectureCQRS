using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.LeaveRequests.Events;

namespace CleanArch.Api.Features.LeaveRequests.LeaveRequestCreated;

internal sealed class LeaveRequestCreatedDomainEventHandler : IDomainEventHandler<LeaveRequestCreatedDomainEvent>
{
    public async Task Handle(LeaveRequestCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // publish an integration event
        await Task.CompletedTask;
    }
}
