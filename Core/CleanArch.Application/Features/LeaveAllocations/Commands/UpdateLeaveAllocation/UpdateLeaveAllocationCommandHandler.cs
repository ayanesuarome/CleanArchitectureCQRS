using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Features.LeaveAllocations.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler(
    IMapper mapper,
    ILeaveAllocationRepository repository,
    IValidator<UpdateLeaveAllocationCommand> validator)
    : IRequestHandler<UpdateLeaveAllocationCommand, Result>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveAllocationRepository _repository = repository;
    private readonly IValidator<UpdateLeaveAllocationCommand> _validator = validator;

    public async Task<Result> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(request.Id);

        if(leaveAllocation == null)
        {
            return new NotFoundResult(LeaveAllocationErrors.NotFound(request.Id));
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveAllocation)}", validationResult);
        }

        _mapper.Map(request, leaveAllocation);
        await _repository.UpdateAsync(leaveAllocation);

        return new SuccessResult();
    }
}
