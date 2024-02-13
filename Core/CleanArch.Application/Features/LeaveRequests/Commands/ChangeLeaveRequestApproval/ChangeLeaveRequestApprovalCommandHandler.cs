using CleanArch.Application.Exceptions;
using CleanArch.Application.Extensions;
using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models.Emails;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand>
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IUserService _userService;
    private readonly IEmailSender _emailSender;
    private readonly EmailTemplateIds _emailTemplateSettings;
    private readonly IValidator<ChangeLeaveRequestApprovalCommand> _validator;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommand> _logger;

    public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository repository,
        IUserService userService,
        IEmailSender emailSender,
        IOptions<EmailTemplateIds> emailTemplateSettings,
        IValidator<ChangeLeaveRequestApprovalCommand> validator,
        IAppLogger<ChangeLeaveRequestApprovalCommand> logger)
    {
        _repository = repository;
        _userService = userService;
        _emailSender = emailSender;
        _emailTemplateSettings = emailTemplateSettings.Value;
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

        if(leaveRequest.IsCancelled)
        {
            validationResult.AddError(
                nameof(leaveRequest.IsCancelled),
                "This leave request has been cancelled and its approval state cannot be updated");

            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }
        
        leaveRequest.IsApproved = request.Approved;
        await _repository.UpdateAsync(leaveRequest);

        try
        {
            Employee employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);
            
            // send confirmation email
            EmailMessageTemplate email = new()
            {
                To = employee.Email,
                TemplateId = _emailTemplateSettings.LeaveRequestApproval,
                TemplateData = new EmailMessageChangeApprovalDto
                {
                    RecipientName = employee.GetName(),
                    Start = leaveRequest.StartDate,
                    End = leaveRequest.EndDate,
                    IsApproved = leaveRequest.IsApproved,
                    Now = DateTimeOffset.Now
                }
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
