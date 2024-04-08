﻿using AutoMapper;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
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

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            LeaveType leaveType = await _repository.GetByIdAsync(request.Id);

            if (leaveType is null)
            {
                return new NotFoundResult<Unit>(LeaveTypeErrors.NotFound(request.Id));
            }

            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<Unit>(LeaveTypeErrors.InvalidLeaveType(validationResult.ToDictionary()));
            }

            _mapper.Map(request, leaveType);
            await _repository.UpdateAsync(leaveType);

            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}