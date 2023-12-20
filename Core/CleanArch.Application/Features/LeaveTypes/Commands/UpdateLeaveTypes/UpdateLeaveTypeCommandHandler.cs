﻿using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveTypes;

public class UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repository)
    : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // TODO: use Benchmark to calculate whether find or any works faster
        LeaveType leaveType = await _repository.GetByIdAsync(request.Id);

        if (leaveType == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        UpdateLeaveTypeCommandValidator validator = new(_repository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveType)}", validationResult);
        }

        _mapper.Map(request, leaveType);
        await _repository.UpdateAsync(leaveType);
        return Unit.Value;
    }
}
