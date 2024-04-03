using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler(
        IMapper mapper,
        ILeaveAllocationRepository repository,
        IValidator<UpdateLeaveAllocationCommand> validator)
        : IRequestHandler<UpdateLeaveAllocationCommand>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveAllocationRepository _repository = repository;
        private readonly IValidator<UpdateLeaveAllocationCommand> _validator = validator;

        public async Task Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _repository.GetByIdAsync(request.Id);

            if(leaveAllocation is null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                throw new BadRequestException($"Invalid {nameof(LeaveAllocation)}", validationResult);
            }

            _mapper.Map(request, leaveAllocation);
            await _repository.UpdateAsync(leaveAllocation);
        }
    }
}
