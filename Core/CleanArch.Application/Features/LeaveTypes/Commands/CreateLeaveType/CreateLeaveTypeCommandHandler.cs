using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(IMapper mapper,
    ILeaveTypeRepository repository,
    IValidator<CreateLeaveTypeCommand> validator)
    : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _repository = repository;
    private readonly IValidator<CreateLeaveTypeCommand> _validator = validator;

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveType)}", validationResult);
        }

        LeaveType leaveTypeToCreate = _mapper.Map<LeaveType>(request);
        await _repository.CreateAsync(leaveTypeToCreate);

        return leaveTypeToCreate.Id;
    }
}
