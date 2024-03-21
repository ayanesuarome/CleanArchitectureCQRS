using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Features.LeaveTypes.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler(
    IMapper mapper,
    ILeaveTypeRepository repository,
    IValidator<UpdateLeaveTypeCommand> validator)
    : IRequestHandler<UpdateLeaveTypeCommand, Result<Unit>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;
    private readonly IValidator<UpdateLeaveTypeCommand> _validator = validator;

    public async Task<Result<Unit>> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        LeaveType leaveType = await _repository.GetByIdAsync(request.Id);

        if (leaveType == null)
        {
            return new NotFoundResult<Unit>(LeaveTypeErrors.NotFound(request.Id));
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveType)}", validationResult);
        }

        _mapper.Map(request, leaveType);
        await _repository.UpdateAsync(leaveType);

        return new SuccessResult<Unit>(Unit.Value);
    }
}
