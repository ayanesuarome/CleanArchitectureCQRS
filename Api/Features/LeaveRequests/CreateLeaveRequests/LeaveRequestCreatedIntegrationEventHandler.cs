﻿using CleanArch.Application.EventBus;
using CleanArch.IntegrationEvents;
using CleanArch.Contracts.Identity;
using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Email;
using CleanArch.Infrastructure.Emails.Options;
using CleanArch.Api.Contracts.Emails;
using Microsoft.Extensions.Options;

namespace CleanArch.Api.Features.LeaveRequests.NotifyLeaveRequestActions;

public class LeaveRequestCreatedIntegrationEventHandler(
    IUserService userService,
    IEmailSender emailSender,
    IOptions<EmailTemplateIdOptions> emailTemplateOptions) : IIntegrationEventHandler<LeaveRequestCreatedIntegrationEvent>
{
    public async Task Handle(LeaveRequestCreatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        Employee employee = await userService.GetEmployee(integrationEvent.EmployeeId);

        EmailMessageTemplate email = new()
        {
            To = employee.Email,
            TemplateId = emailTemplateOptions.Value.LeaveRequestCreate,
            TemplateData = new EmailMessageDto(
                RecipientName: employee.FullName,
                Start: integrationEvent.StartDate,
                End: integrationEvent.EndDate,
                Now: integrationEvent.OcurredOn)
        };

        await emailSender.SendEmail(email);
    }
}
