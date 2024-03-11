using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, LeaveRequest>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _repository;
    private readonly IValidator<UpdateLeaveRequestCommand> _validator;

    public UpdateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveRequestRepository repository,
        IValidator<UpdateLeaveRequestCommand> validator)
    {
        _mapper = mapper;
        _repository = repository;
        _validator = validator;
    }

    public async Task<LeaveRequest> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetByIdAsync(request.Id);

        if(leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }

        _mapper.Map(request, leaveRequest);
        await _repository.UpdateAsync(leaveRequest);

        return leaveRequest;
    }
}
