using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Email;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Interfaces.Logging;
using CleanArch.Application.Models.Emails;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Options;

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

        leaveRequest.IsApproved = request.Approved;
        await _repository.UpdateAsync(leaveRequest);

        try
        {
            // send confirmation email
            EmailMessageTemplate email = new()
            {
                To = "ayanesuarome@yahoo.com", // TODO: get email from employee record
                TemplateId = _emailTemplateSettings.LeaveRequestApproval,
                TemplateData = new
                {
                    //recipientName = // TODO: get name
                    start = leaveRequest.StartDate.Date.ToShortDateString(),
                    end = leaveRequest.EndDate.Date.ToShortDateString(),
                    isApproved = leaveRequest.IsApproved
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
