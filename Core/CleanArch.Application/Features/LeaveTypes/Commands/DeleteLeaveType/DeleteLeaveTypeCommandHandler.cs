using CleanArch.Application.Features.LeaveTypes.Shared;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository)
    : IRequestHandler<DeleteLeaveTypeCommand, Result<Unit>>
{
    private readonly ILeaveTypeRepository _repository = repository;

    public async Task<Result<Unit>> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        LeaveType leaveTypeToDelete = await _repository.GetByIdAsync(request.Id);

        if(leaveTypeToDelete == null)
        {
            return new NotFoundResult<Unit>(LeaveTypeErrors.NotFound(request.Id));
        }

        await _repository.DeleteAsync(leaveTypeToDelete);
        return new SuccessResult<Unit>(Unit.Value);
    }
}
