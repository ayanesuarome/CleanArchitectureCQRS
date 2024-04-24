using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    internal sealed class Handler(ILeaveTypeRepository repository, IValidator<Command> validator)
        : IRequestHandler<Command, Result<Guid>>
    {
        private readonly ILeaveTypeRepository _repository = repository;
        private readonly IValidator<Command> _validator = validator;

        public async Task<Result<Guid>> Handle(Command command, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<Guid>(ValidationErrors.CreateLeaveType.CreateLeaveTypeValidation(validationResult.ToString()));
            }

            Result<Name> nameResult = Name.Create(command.Name);
            Result<DefaultDays> defaultDaysResult = DefaultDays.Create(command.DefaultDays);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult, defaultDaysResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return new FailureResult<Guid>(firstFailureOrSuccess.Error);
            }

            if (!await _repository.IsUniqueAsync(nameResult.Value, cancellationToken))
            {
                return new FailureResult<Guid>(DomainErrors.LeaveType.DuplicateName);
            }

            LeaveType leaveTypeToCreate = new(nameResult.Value, defaultDaysResult.Value);

            await _repository.CreateAsync(leaveTypeToCreate);

            return new SuccessResult<Guid>(leaveTypeToCreate.Id);
        }
    }
}
