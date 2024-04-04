using AutoMapper;
using CleanArch.Application.Features.LeaveTypes.Shared;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(IMapper mapper,
    ILeaveTypeRepository repository,
    IValidator<CreateLeaveTypeCommand> validator)
    : IRequestHandler<CreateLeaveTypeCommand, Result<int>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;
    private readonly IValidator<CreateLeaveTypeCommand> _validator = validator;

    public async Task<Result<int>> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            return new FailureResult<int>(LeaveTypeErrors.InvalidLeaveType(validationResult.ToDictionary()));
        }

        LeaveType leaveTypeToCreate = _mapper.Map<LeaveType>(request);
        await _repository.CreateAsync(leaveTypeToCreate);

        return new SuccessResult<int>(leaveTypeToCreate.Id);
    }
}
