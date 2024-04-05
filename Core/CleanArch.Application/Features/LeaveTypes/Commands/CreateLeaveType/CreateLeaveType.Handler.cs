using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;

public static partial class CreateLeaveType
{
    internal sealed class Handler(IMapper mapper,
    ILeaveTypeRepository repository,
    IValidator<Command> validator)
    : IRequestHandler<Command, int>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = repository;
        private readonly IValidator<Command> _validator = validator;

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException($"Invalid {nameof(LeaveType)}", validationResult);
            }

            LeaveType leaveTypeToCreate = _mapper.Map<LeaveType>(request);
            await _repository.CreateAsync(leaveTypeToCreate);

            return leaveTypeToCreate.Id;
        }
    }
}
