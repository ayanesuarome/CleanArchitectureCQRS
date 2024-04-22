using CleanArch.Application.Extensions;
using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeRepository _repository;

        public Validator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            RuleFor(m => m.Id)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveType.IdIsRequired);

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveType.NameIsRequired);

            RuleFor(m => m.DefaultDays)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveType.DefaultDaysIsRequired);
        }
    }
}
