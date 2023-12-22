using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository repository) : IRequestHandler<DeleteLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _repository = repository;

    public async Task Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(request.Id);

        if (leaveAllocation == null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        await _repository.DeleteAsync(leaveAllocation);
    }
}
