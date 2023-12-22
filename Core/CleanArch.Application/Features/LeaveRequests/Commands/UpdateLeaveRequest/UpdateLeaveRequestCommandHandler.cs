using AutoMapper;
using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _repository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

    public UpdateLeaveRequestCommandHandler(IMapper mapper,
        ILeaveRequestRepository repository,
        IEmailSender emailSender,
        IAppLogger<UpdateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetByIdAsync(request.Id);

        if(leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        UpdateLeaveRequestCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }

        _mapper.Map(request, leaveRequest);
        await _repository.UpdateAsync(leaveRequest);

        try
        {
            // send confirmation email
            EmailMessage email = new()
            {
                To = string.Empty, // TODO: get email from employee record
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                        $"has been updated successfully.",
                Subject = "Leave Request Updated"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
