using CleanArch.Application.Abstractions.Email;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.Abstractions.Logging;
using MediatR;
using Microsoft.Extensions.Options;
using CleanArch.Domain.Events;
using CleanArch.Api.Contracts.Emails;
using CleanArch.Infrastructure.Services.Emails.Settings;
using CleanArch.Contracts.Identity;

namespace CleanArch.Api.Features.LeaveRequests.NotifyLeaveRequestActions;

public class NotifyLeaveRequestAction : INotificationHandler<LeaveRequestEvent>
{
    private readonly IUserService _userService;
    private readonly IEmailSender _emailSender;
    private readonly EmailTemplateIds _emailTemplateSettings;
    private readonly IAppLogger<NotifyLeaveRequestAction> _logger;

    public NotifyLeaveRequestAction(
        IUserService userService,
        IEmailSender emailSender,
        IOptions<EmailTemplateIds> emailTemplateSettings,
        IAppLogger<NotifyLeaveRequestAction> logger)
    {
        _userService = userService;
        _emailSender = emailSender;
        _emailTemplateSettings = emailTemplateSettings.Value;
        _logger = logger;
    }

    public async Task Handle(LeaveRequestEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            Employee employee = await _userService.GetEmployee(notification.LeaveRequest.RequestingEmployeeId);

            string templateId = notification.Action switch
            {
                LeaveRequestAction.Created => _emailTemplateSettings.LeaveRequestCreate,
                LeaveRequestAction.Updated => _emailTemplateSettings.LeaveRequestUpdate,
                LeaveRequestAction.Canceled => _emailTemplateSettings.LeaveRequestCancelation,
                LeaveRequestAction.UpdateApproval => _emailTemplateSettings.LeaveRequestApproval,
            };

            // send confirmation email
            EmailMessageTemplate email = new()
            {
                To = employee.Email,
                TemplateId = templateId,
                TemplateData = new EmailMessageDto(
                    RecipientName: employee.GetName(),
                    Start: notification.LeaveRequest.StartDate,
                    End: notification.LeaveRequest.EndDate,
                    Now: notification.ActionDate
                    )
                {
                    IsApproved = notification.LeaveRequest.IsApproved
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
