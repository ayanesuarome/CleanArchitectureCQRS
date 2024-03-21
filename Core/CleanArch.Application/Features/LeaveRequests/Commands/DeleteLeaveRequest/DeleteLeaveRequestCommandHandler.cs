using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler(ILeaveRequestRepository repository)
    : IRequestHandler<DeleteLeaveRequestCommand, Result>
{
    private readonly ILeaveRequestRepository _repository = repository;

    public async Task<Result> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
        {
            return new NotFoundResult(LeaveRequestErrors.NotFound(request.Id));
        }

        await _repository.DeleteAsync(leaveRequest);
        
        return new SuccessResult();
    }
}
