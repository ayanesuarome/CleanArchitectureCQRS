using AutoMapper;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    internal sealed class Handler : IRequestHandler<Command, Result<LeaveRequest>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _repository;
        private readonly IValidator<Command> _validator;

        public Handler(IMapper mapper,
            ILeaveRequestRepository repository,
            IValidator<Command> validator)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<LeaveRequest>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetByIdAsync(command.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult<LeaveRequest>(LeaveRequestErrors.NotFound(command.Id));
            }

            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<LeaveRequest>(
                    LeaveRequestErrors.UpdateLeaveRequestValidation(validationResult.ToString()));
            }

            _mapper.Map(command, leaveRequest);
            await _repository.UpdateAsync(leaveRequest);

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
