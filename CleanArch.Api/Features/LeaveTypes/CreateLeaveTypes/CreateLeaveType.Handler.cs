using AutoMapper;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    internal sealed class Handler(IMapper mapper,
    ILeaveTypeRepository repository,
    IValidator<Command> validator)
    : IRequestHandler<Command, Result<int>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = repository;
        private readonly IValidator<Command> _validator = validator;

        public async Task<Result<int>> Handle(Command commnad, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(commnad, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<int>(LeaveTypeErrors.CreateLeaveTypeValidation(validationResult.ToString()));
            }

            LeaveType leaveTypeToCreate = _mapper.Map<LeaveType>(commnad);
            await _repository.CreateAsync(leaveTypeToCreate);

            return new SuccessResult<int>(leaveTypeToCreate.Id);
        }
    }
}
