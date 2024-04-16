﻿using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveRequests;

public sealed class BaseCommandValidator : AbstractValidator<BaseCommand>
{
    public BaseCommandValidator()
    {
        RuleFor(m => m.StartDate)
            .LessThan(m => m.EndDate);
        //.WithError(ValidationErrors.StartDateLowerThanEndDate);

        RuleFor(m => m.EndDate)
            .GreaterThan(m => m.StartDate);
            //.WithError(ValidationErrors.EndDateGeatherThanStartDate);
    }
}
