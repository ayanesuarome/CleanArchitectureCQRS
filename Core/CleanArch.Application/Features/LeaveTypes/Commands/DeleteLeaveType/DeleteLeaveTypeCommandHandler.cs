using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository)
    : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _repository = repository;

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        LeaveType leaveTypeToDelete = await _repository.GetByIdAsync(request.Id);

        if(leaveTypeToDelete is null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        await _repository.DeleteAsync(leaveTypeToDelete);
        return Unit.Value;
    }
}
