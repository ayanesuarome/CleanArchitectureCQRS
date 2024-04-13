using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocations;

public static partial class UpdateLeaveAllocation
{
    internal sealed class Handler(IMapper mapper, ILeaveAllocationRepository repository, IValidator<Command> validator)
        : IRequestHandler<Command, Result>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveAllocationRepository _repository = repository;
        private readonly IValidator<Command> _validator = validator;

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(command.Id);

            if (leaveAllocation is null)
            {
                return new NotFoundResult(LeaveAllocationErrors.NotFound(command.Id));
            }

            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult(LeaveAllocationErrors.UpdateLeaveAllocationValidation(validationResult.ToString()));
            }

            _mapper.Map(command, leaveAllocation);
            await _repository.UpdateAsync(leaveAllocation);

            return new SuccessResult();
        }
    }
}
