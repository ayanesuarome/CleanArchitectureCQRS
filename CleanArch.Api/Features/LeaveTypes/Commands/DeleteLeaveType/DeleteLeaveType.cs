using CleanArch.Api.Features.LeaveTypes.Shared;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.Commands.DeleteLeaveType
{
    public static partial class DeleteLeaveType
    {
        public record Command(int Id) : IRequest<Result<Unit>>
        {
        }

        internal sealed class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository)
            : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ILeaveTypeRepository _repository = repository;

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                LeaveType leaveTypeToDelete = await _repository.GetByIdAsync(request.Id);

                if (leaveTypeToDelete is null)
                {
                    return new NotFoundResult<Unit>(LeaveTypeErrors.NotFound(request.Id));
                }

                await _repository.DeleteAsync(leaveTypeToDelete);
                return new SuccessResult<Unit>(Unit.Value);
            }
        }

    }
}
