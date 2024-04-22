using CleanArch.Application.Extensions;
using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeRepository _repository;

        public Validator(ILeaveTypeRepository repository)
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithError(ValidationErrors.CreateLeaveType.NameIsRequired);

            RuleFor(m => m.DefaultDays)
                .NotEmpty()
                .WithError(ValidationErrors.CreateLeaveType.DefaultDaysIsRequired);

            _repository = repository;
        }
    }
}
