using CleanArch.Application.Extensions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
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
                .Cascade(CascadeMode.Stop)
                .MaximumLength(70)
                    .WithError(ValidationErrors.UpdateLeaveType.NameMaximumLength("{MaxLength}"))
                .MustAsync(LeaveTypeUniqueName)
                    .WithError(DomainErrors.LeaveType.DuplicateName);

            RuleFor(m => m.DefaultDays)
                .InclusiveBetween(1, 100)
                    .WithError(ValidationErrors.UpdateLeaveType.DefaultDaysRange("{From} - {To}"));
        }

        private async Task<bool> LeaveTypeUniqueName(Command command, string name, CancellationToken cancellation)
        {
            if(!string.IsNullOrEmpty(name))
            {
                LeaveType leaveType = await _repository.GetByIdAsync(command.Id);

                if (leaveType.Name != name)
                {
                    return await _repository.IsUniqueAsync(name, cancellation);
                }
            }           

            return true;
        }
    }
}
