using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    internal sealed class Handler(
        IMapper mapper,
        ILeaveTypeRepository repository,
        IValidator<Command> validator)
        : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = repository;
        private readonly IValidator<Command> _validator = validator;

        public async Task<Result<Unit>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveType leaveType = await _repository.GetByIdAsync(command.Id);

            if (leaveType is null)
            {
                return new NotFoundResult<Unit>(DomainErrors.LeaveType.NotFound(command.Id));
            }

            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<Unit>(ValidationErrors.UpdateLeaveType.UpdateLeaveTypeValidation(validationResult.ToString()));
            }

            _mapper.Map(command, leaveType);
            await _repository.UpdateAsync(leaveType);

            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
