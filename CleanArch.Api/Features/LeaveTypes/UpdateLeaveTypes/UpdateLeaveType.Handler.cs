using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    public sealed class Handler(
        ILeaveTypeRepository repository,
        IValidator<Command> validator)
        : IRequestHandler<Command, Result<Unit>>
    {
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

            Result<Name> nameResult = Name.Create(command.Name);
            Result<DefaultDays> defaultDaysResult = DefaultDays.Create(command.DefaultDays);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult, defaultDaysResult);

            if(firstFailureOrSuccess.IsFailure)
            {
                return new FailureResult<Unit>(firstFailureOrSuccess.Error);
            }

            Result updateNameResult = await leaveType.UpdateName(nameResult.Value, repository);

            if(updateNameResult.IsFailure)
            {
                return new FailureResult<Unit>(updateNameResult.Error);
            }

            leaveType.UpdateDefaultDays(defaultDaysResult.Value);

            _repository.Update(leaveType);

            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
