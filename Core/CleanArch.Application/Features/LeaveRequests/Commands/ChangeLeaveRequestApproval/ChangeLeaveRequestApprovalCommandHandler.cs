using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand>
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IEmailSender _emailSender;
    private readonly IValidator<ChangeLeaveRequestApprovalCommand> _validator;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommand> _logger;

    public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository repository,
        IEmailSender emailSender,
        IValidator<ChangeLeaveRequestApprovalCommand> validator,
        IAppLogger<ChangeLeaveRequestApprovalCommand> logger)
    {
        _repository = repository;
        _emailSender = emailSender;
        _validator = validator;
        _logger = logger;
    }

    public async Task Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _repository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid approval request", validationResult);
        }

        leaveRequest.IsApproved = request.Approved;
        await _repository.UpdateAsync(leaveRequest);

        try
        {
            // TODO: use email templates
            // send confirmation email
            EmailMessage email = new()
            {
                To = "ayanesuarome@yahoo.com", // TODO: get email from employee record
                Body = $"The approval status for your leave request for" +
                        $" {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                        $"has been updated.",
                Subject = "Leave Request Approval Status Updated"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
